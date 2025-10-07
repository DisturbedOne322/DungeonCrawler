using System;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UniRx;

namespace Gameplay.Buffs
{
    public class WeaponBuffApplier : IDisposable
    {
        private readonly UnitBuffsData _unitBuffsData;
        private readonly UnitEquipmentData _unitEquipmentData;

        private CompositeDisposable _compositeDisposable = new();
        
        public WeaponBuffApplier(UnitBuffsData unitBuffsData, UnitEquipmentData unitEquipmentData)
        {
            _unitBuffsData = unitBuffsData;
            _unitEquipmentData = unitEquipmentData;
            
            _compositeDisposable.Add(
                _unitEquipmentData.OnWeaponEquipped.Subscribe(EquipBuffFromWeapon)
                );
            
            _compositeDisposable.Add(
                _unitEquipmentData.OnArmorEquipped.Subscribe(EquipBuffFromArmor)
            );
            
            _compositeDisposable.Add(
                _unitEquipmentData.OnWeaponRemoved.Subscribe(RemoveBuffFromWeapon)
            );
            
            _compositeDisposable.Add(
                _unitEquipmentData.OnArmorRemoved.Subscribe(RemoveBuffFromArmor)
            );
        }

        private void EquipBuffFromWeapon(BaseWeapon weapon)
        {
            if(weapon.OffensiveBuff)
                _unitBuffsData.OffensiveBuffs.Add(weapon.OffensiveBuff);
            
            if(weapon.DefensiveBuff)
                _unitBuffsData.DefensiveBuffs.Add(weapon.DefensiveBuff);
        }
        
        private void EquipBuffFromArmor(BaseArmor armor)
        {
            if(armor.OffensiveBuff)
                _unitBuffsData.OffensiveBuffs.Add(armor.OffensiveBuff);
            
            if(armor.DefensiveBuff)
                _unitBuffsData.DefensiveBuffs.Add(armor.DefensiveBuff);
        }
        
        private void RemoveBuffFromWeapon(BaseWeapon weapon)
        {
            if(weapon.OffensiveBuff)
                _unitBuffsData.OffensiveBuffs.Remove(weapon.OffensiveBuff);
            
            if(weapon.DefensiveBuff)
                _unitBuffsData.DefensiveBuffs.Remove(weapon.DefensiveBuff);
        }
        
        private void RemoveBuffFromArmor(BaseArmor armor)
        {
            if(armor.OffensiveBuff)
                _unitBuffsData.OffensiveBuffs.Remove(armor.OffensiveBuff);
            
            if(armor.DefensiveBuff)
                _unitBuffsData.DefensiveBuffs.Remove(armor.DefensiveBuff);
        }

        public void Dispose() => _compositeDisposable.Dispose();
    }
}