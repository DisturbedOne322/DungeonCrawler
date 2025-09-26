using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;

namespace Gameplay.Equipment
{
    public class UnitEquipmentData
    {
        private BaseWeapon _weapon;
        private BaseArmor _armor;

        public bool IsWeaponEquipped() => _weapon;
        public bool IsArmorEquipped() => _armor;
        
        public BaseWeapon GetEquippedWeapon() => _weapon;
        public BaseArmor GetEquippedArmor() => _armor;

        public void EquipWeapon(BaseWeapon weapon) => _weapon = weapon;
        public void EquipArmor(BaseArmor armor) => _armor = armor;
    }
}