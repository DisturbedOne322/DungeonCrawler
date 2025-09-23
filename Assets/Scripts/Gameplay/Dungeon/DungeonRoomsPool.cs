using System;
using System.Collections.Generic;
using Data;
using Gameplay.Dungeon.Rooms;
using UniRx;

namespace Gameplay.Dungeon
{
    public class DungeonRoomsPool : IDisposable
    {
        private readonly GameplayData _gameplayData;
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;
        
        private readonly Dictionary<RoomType, List<DungeonRoom>> _dictionary = new ();
        
        private readonly IDisposable _disposable;

        public DungeonRoomsPool(GameplayData gameplayData, DungeonLayoutProvider dungeonLayoutProvider)
        {
            _dungeonLayoutProvider = dungeonLayoutProvider;

            _disposable = gameplayData.PlayerPositionIndex.Subscribe(TryReturnRoomToPool);
        }
        
        public bool TryGetRoom(RoomType roomType, out DungeonRoom room)
        {
            if (_dictionary.TryGetValue(roomType, out var list) && list.Count > 0)
            {
                room = list[0];
                list.RemoveAt(0);
                room.gameObject.SetActive(true);
                return true;
            }
                
            room = null;
            return false;
        }

        private void TryReturnRoomToPool(int playerIndex)
        {
            int roomIndex = playerIndex - 2;
            
            if (!_dungeonLayoutProvider.TryGetRoom(roomIndex, out var room))
                return;

            room.ResetRoom();
            room.gameObject.SetActive(false);
            
            if(_dictionary.TryGetValue(room.RoomType, out var list))
                list.Add(room);
            else
                _dictionary.Add(room.RoomType, new () { room });
        }

        public void Dispose() => _disposable.Dispose();
    }
}