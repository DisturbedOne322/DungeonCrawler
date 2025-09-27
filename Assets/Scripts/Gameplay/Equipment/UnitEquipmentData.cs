using System;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UniRx;

namespace Gameplay.Equipment
{
    public class UnitEquipmentData
    {
        private BaseWeapon _weapon;
        private BaseArmor _armor;

        public readonly Subject<BaseWeapon> OnWeaponEquipped = new();
        public readonly Subject<BaseArmor> OnArmorEquipped = new();

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
            _weapon = weapon;
            OnWeaponEquipped?.OnNext(_weapon);
        }

        public void EquipArmor(BaseArmor armor)
        {
            _armor = armor;
            OnArmorEquipped?.OnNext(_armor);
        }
    }
}