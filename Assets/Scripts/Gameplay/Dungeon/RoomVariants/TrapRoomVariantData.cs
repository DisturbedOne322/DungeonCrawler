using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    public class TrapRoomVariantData : RoomVariantData
    {
        [SerializeField] private List<GameObject> _trapPrefabs;
        [SerializeField, Range(0, 1f)] private float _trapChance = 0.5f;
        
        public IReadOnlyList<GameObject> TrapPrefabs => _trapPrefabs;
        public float TrapChance => _trapChance;
        
        public override RoomType RoomType => RoomType.Trap;
    }
}