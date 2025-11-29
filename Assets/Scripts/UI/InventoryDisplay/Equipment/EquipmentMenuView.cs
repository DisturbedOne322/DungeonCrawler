using System;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace UI.InventoryDisplay
{
    public class EquipmentMenuView : BaseDisplayMenuView
    {
        [SerializeField] private EquipmentView _weaponDataView;
        [SerializeField] private EquipmentView _armorDataView;

        private PlayerUnit _playerUnit;

        [Inject]
        private void Construct(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
        }

        protected override void Initialize()
        {
            DisplayWeapon();
            DisplayArmor();
        }

        private void DisplayArmor()
        {
            if(_playerUnit.UnitEquipmentData.TryGetEquippedArmor(out var armor))
                _armorDataView.SetEquipped(armor);
            else
                _armorDataView.SetUnequipped();
        }

        private void DisplayWeapon()
        {
            if(_playerUnit.UnitEquipmentData.TryGetEquippedWeapon(out var weapon))
                _weaponDataView.SetEquipped(weapon);
            else
                _weaponDataView.SetUnequipped();
        }
    }
}