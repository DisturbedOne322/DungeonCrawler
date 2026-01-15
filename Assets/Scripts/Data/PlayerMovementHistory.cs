using Helpers;
using UniRx;
using UnityEngine;

namespace Data
{
    public class PlayerMovementHistory
    {
        public ReactiveCollection<RoomType> AllRoomsHistory { get; } = new();

        public int AllRoomsCount => AllRoomsHistory.Count;

        public int Depth { get; private set; } = 0;

        public void AddRoom(RoomType roomType)
        {
            AllRoomsHistory.Add(roomType);

            if (RoomTypeHelper.IsRecordableRoom(roomType))
                Depth++;
        }
    }
}