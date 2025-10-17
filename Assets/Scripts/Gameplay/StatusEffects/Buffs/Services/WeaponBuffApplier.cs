using System;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class WeaponBuffApplier : IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly UnitBuffsData _unitBuffsData;
        private readonly UnitDebuffsData _unitDebuffsData;

        public WeaponBuffApplier(UnitBuffsData unitBuffsData, UnitDebuffsData unitDebuffsData, UnitEquipmentData unitEquipmentData)
        {
            _unitBuffsData = unitBuffsData;
            _unitDebuffsData = unitDebuffsData;

            _compositeDisposable.Add(unitEquipmentData.OnWeaponEquipped.Subscribe(EquipBuffsFromWeapon));
            _compositeDisposable.Add(unitEquipmentData.OnArmorEquipped.Subscribe(EquipBuffsFromArmor));
            _compositeDisposable.Add(unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveBuffsFromWeapon));
            _compositeDisposable.Add(unitEquipmentData.OnArmorRemoved.Subscribe(RemoveBuffsFromArmor));
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }

        private void EquipBuffsFromWeapon(BaseWeapon weapon)
        {
            EquipBuffsFromEquipment(weapon);
            EquipDebuffsFromEquipment(weapon);
        }

        private void EquipBuffsFromArmor(BaseArmor armor)
        {
            EquipBuffsFromEquipment(armor);
            EquipDebuffsFromEquipment(armor);
        }
        
        private void RemoveBuffsFromWeapon(BaseWeapon weapon)
        {
            RemoveBuffsFromEquipment(weapon);
            RemoveDebuffsFromEquipment(weapon);
        }

        private void RemoveBuffsFromArmor(BaseArmor armor)
        {
            RemoveBuffsFromEquipment(armor);
            RemoveDebuffsFromEquipment(armor);
        }

        private void EquipBuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var buff in equipmentPiece.OffensiveBuffs)
                if (buff)
                    _unitBuffsData.OffensiveBuffs.Add(buff);

            foreach (var buff in equipmentPiece.DefensiveBuffs)
                if (buff)
                    _unitBuffsData.DefensiveBuffs.Add(buff);

            foreach (var buff in equipmentPiece.StatBuffs)
                if (buff)
                    _unitBuffsData.StatBuffs.Add(buff);
        }
        
        private void EquipDebuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var debuff in equipmentPiece.StatDebuffs)
                if (debuff)
                    _unitDebuffsData.StatDebuffs.Add(debuff);
        }

        private void RemoveBuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var buff in equipmentPiece.OffensiveBuffs)
                if (buff)
                    _unitBuffsData.OffensiveBuffs.Remove(buff);

            foreach (var buff in equipmentPiece.DefensiveBuffs)
                if (buff)
                    _unitBuffsData.DefensiveBuffs.Remove(buff);

            foreach (var buff in equipmentPiece.StatBuffs)
                if (buff)
                    _unitBuffsData.StatBuffs.Remove(buff);
        }
        
        private void RemoveDebuffsFromEquipment(BaseEquipmentPiece equipmentPiece)
        {
            foreach (var debuff in equipmentPiece.StatDebuffs)
                if (debuff)
                    _unitDebuffsData.StatDebuffs.Remove(debuff);
        }
    }
}