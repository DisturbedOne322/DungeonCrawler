using UnityEngine;

namespace Gameplay.Equipment.Weapons
{
    public abstract class BaseWeapon : BaseEquipmentPiece
    {
        [SerializeField] private GameObject _prefab;

        public GameObject Prefab => _prefab;
    }
}