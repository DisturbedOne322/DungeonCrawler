using System;
using Gameplay.Combat.Data;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UniRx;
using UnityEngine;

namespace Gameplay.Equipment
{
    public class UnitEquipmentData
    {
        private BaseWeapon _weapon;
        private BaseArmor _armor;

        public readonly Subject<BaseWeapon> OnWeaponEquipped = new();
        public readonly Subject<BaseWeapon> OnWeaponRemoved = new();
        
        public readonly Subject<BaseArmor> OnArmorEquipped = new();
        public readonly Subject<BaseArmor> OnArmorRemoved = new();

        public bool TryGetEquippedWeapon(out BaseWeapon weapon)
        {
            weapon = _weapon;
            return _weapon;
        }

        public bool TryGetEquippedArmor(out BaseArmor armor)
        {
            armor = _armor;
            return _armor;
        }
        
        public void EquipWeapon(BaseWeapon weapon)
        {
            TryRemoveWeapon();
            
            _weapon = weapon;
            OnWeaponEquipped?.OnNext(_weapon);
        }

        public void EquipArmor(BaseArmor armor)
        {
            TryRemoveArmor();
            
            _armor = armor;
            OnArmorEquipped?.OnNext(_armor);
        }

        private void TryRemoveWeapon()
        {
            if(!_weapon)
                return;
            
            OnWeaponRemoved?.OnNext(_weapon);
        }
        
        private void TryRemoveArmor()
        {
            if(!_armor)
                return;
            
            OnArmorRemoved?.OnNext(_armor);
        }
    }
}