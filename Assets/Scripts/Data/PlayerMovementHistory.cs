using UniRx;

namespace Data
{
    public class PlayerMovementHistory
    {
        private readonly ReactiveCollection<RoomType> _roomsHistory = new();
        
        public ReactiveCollection<RoomType> RoomsHistory => _roomsHistory;
        
        public void AddRoom(RoomType roomType) => _roomsHistory.Add(roomType);
    }
}