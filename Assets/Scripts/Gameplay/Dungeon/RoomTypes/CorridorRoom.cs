namespace Gameplay.Dungeon.RoomTypes
{
    public class CorridorRoom : DungeonRoom
    {
        private RoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        
        public void SetData(RoomVariantData data) => _roomData = data;
    }
}