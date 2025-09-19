using Data;
using UnityEngine;

namespace Gameplay.Dungeon.Areas
{
    public abstract class DungeonArea : MonoBehaviour
    {
        [SerializeField] private Transform _playerStandPoint;
        
        public Transform PlayerStandPoint => _playerStandPoint;

        public abstract RoomType RoomType { get; }
    }
}