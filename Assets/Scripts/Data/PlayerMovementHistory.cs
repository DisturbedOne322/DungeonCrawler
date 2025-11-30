using UniRx;

namespace Data
{
    public class PlayerMovementHistory
    {
        public ReactiveCollection<RoomType> RoomsHistory { get; } = new();

        public int Depth => RoomsHistory.Count;
        
        public void AddRoom(RoomType roomType)
        {
            RoomsHistory.Add(roomType);
        }
    }
}