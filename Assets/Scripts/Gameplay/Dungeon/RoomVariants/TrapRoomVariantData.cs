using Data;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    public class TrapRoomVariantData : RoomVariantData
    {
        [SerializeField] private TrapRoomData _trapRoomData;
        
        public TrapRoomData TrapRoomData => _trapRoomData;
        public override RoomType RoomType => RoomType.Trap;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var shrineRoom = room as TrapRoom;
            shrineRoom?.SetData(this);
        }
    }
}