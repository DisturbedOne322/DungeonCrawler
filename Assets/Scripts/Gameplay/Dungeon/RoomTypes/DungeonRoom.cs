using Gameplay.Dungeon.Data;
using UnityEngine;

namespace Gameplay.Dungeon.RoomTypes
{
    [DisallowMultipleComponent]
    public abstract class DungeonRoom : MonoBehaviour
    {
        [SerializeField] private Transform _playerStandPoint;

        public abstract RoomVariantData RoomData { get; }

        public Transform PlayerStandPoint => _playerStandPoint;
        
        public virtual void ResetRoom()
        {
            
        }

        public virtual void SetupRoom()
        {
            
        }
    }
}