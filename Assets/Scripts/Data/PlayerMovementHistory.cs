using UniRx;

namespace Data
{
    public class PlayerMovementHistory
    {
        public ReactiveCollection<RoomType> RoomsHistory { get; } = new();

        public void AddRoom(RoomType roomType)
        {
            RoomsHistory.Add(roomType);
        }
    }
}