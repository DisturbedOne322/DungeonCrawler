using Gameplay.Dungeon.RoomVariants;

namespace Gameplay.Dungeon.Rooms
{
    public class CorridorRoom : DungeonRoom
    {
        private CorridorRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;

        public override void SetData(RoomVariantData data)
        {
            _roomData = data as CorridorRoomVariantData;
        }
    }
}