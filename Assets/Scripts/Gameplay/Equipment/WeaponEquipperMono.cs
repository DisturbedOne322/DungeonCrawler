using Gameplay.Equipment.Weapons;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Equipment
{
    public class WeaponEquipperMono : MonoBehaviour
    {
        [SerializeField] private Transform _pivotPoint;

        private GameObject _equippedWeapon;
        
        private UnitEquipmentData _equipmentData;

        [Inject]
        private void Construct(UnitEquipmentData equipmentData)
        {
            _equipmentData = equipmentData;
            _equipmentData.OnWeaponEquipped.Subscribe(EquipWeapon).AddTo(gameObject);
        }

        private void EquipWeapon(BaseWeapon weapon)
        {
            if(_equippedWeapon)
                UnequipPreviousWeapon();

            var prefab = weapon.Prefab;
            _equippedWeapon = Instantiate(prefab, _pivotPoint);
        }

        private void UnequipPreviousWeapon()
        {
            Destroy(_equippedWeapon);
        }
    }
}