using System.Collections.Generic;
using Data;
using Gameplay.Dungeon.Rooms;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    public class TrapRoomVariantData : RoomVariantData
    {
        [SerializeField] private List<GameObject> _trapPrefabs;
        
        public IReadOnlyList<GameObject> TrapPrefabs => _trapPrefabs;
        
        public override RoomType RoomType => RoomType.Trap;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var trapRoom = room as TrapRoom;
            trapRoom?.SetData(this);
        }
    }
}