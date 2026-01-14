using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using UniRx;

namespace Gameplay.Equipment
{
    public class UnitEquipmentData
    {
        public readonly Subject<BaseArmor> OnArmorEquipped = new();
        public readonly Subject<BaseArmor> OnArmorRemoved = new();

        public readonly Subject<BaseWeapon> OnWeaponEquipped = new();
        public readonly Subject<BaseWeapon> OnWeaponRemoved = new();

        private BaseArmor _armor;
        private BaseWeapon _weapon;

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
            if (!_weapon)
                return;

            OnWeaponRemoved?.OnNext(_weapon);
        }

        private void TryRemoveArmor()
        {
            if (!_armor)
                return;

            OnArmorRemoved?.OnNext(_armor);
        }
    }
}