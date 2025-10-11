using System;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UniRx;

namespace Gameplay.Buffs.Services
{
    public class WeaponBuffApplier : IDisposable
    {
        private readonly UnitBuffsData _unitBuffsData;

        private CompositeDisposable _compositeDisposable = new();
        
        public WeaponBuffApplier(UnitBuffsData unitBuffsData, UnitEquipmentData unitEquipmentData)
        {
            _unitBuffsData = unitBuffsData;

            _compositeDisposable.Add(unitEquipmentData.OnWeaponEquipped.Subscribe(EquipBuffsFromWeapon));
            _compositeDisposable.Add(unitEquipmentData.OnArmorEquipped.Subscribe(EquipBuffsFromArmor));
            _compositeDisposable.Add(unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveBuffsFromWeapon));
            _compositeDisposable.Add(unitEquipmentData.OnArmorRemoved.Subscribe(RemoveBuffsFromArmor));
        }

        private void EquipBuffsFromWeapon(BaseWeapon weapon) => EquipBuffsFromEquipment(weapon);

        private void EquipBuffsFromArmor(BaseArmor armor) => EquipBuffsFromEquipment(armor);

        private void EquipBuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var buff in equipmentPiece.OffensiveBuffs)
                if(buff) _unitBuffsData.OffensiveBuffs.Add(buff);
            
            foreach (var buff in equipmentPiece.DefensiveBuffs)
                if(buff) _unitBuffsData.DefensiveBuffs.Add(buff);
            
            foreach (var buff in equipmentPiece.StatBuffs)
                if(buff) _unitBuffsData.StatBuffs.Add(buff);
        }
        
        private void RemoveBuffsFromWeapon(BaseWeapon weapon) => RemoveBuffsFromEquipment(weapon);

        private void RemoveBuffsFromArmor(BaseArmor armor) => RemoveBuffsFromEquipment(armor);

        private void RemoveBuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var buff in equipmentPiece.OffensiveBuffs)
                if(buff) _unitBuffsData.OffensiveBuffs.Remove(buff);
            
            foreach (var buff in equipmentPiece.DefensiveBuffs)
                if(buff) _unitBuffsData.DefensiveBuffs.Remove(buff);
            
            foreach (var buff in equipmentPiece.StatBuffs)
                if(buff) _unitBuffsData.StatBuffs.Remove(buff);
        }

        public void Dispose() => _compositeDisposable.Dispose();
    }
}