using System.Collections.Generic;
using AssetManagement.AssetProviders.ConfigProviders;
using Data;
using Gameplay.Dungeon.RoomTypes;

namespace Gameplay.Dungeon.Data
{
    public class DungeonRoomsProvider
    {
        private readonly DungeonRoomsDatabase _dungeonRoomsDatabase;
        
        private readonly Dictionary<RoomType, List<DungeonRoom>> _roomDataMap = new();

        public DungeonRoomsProvider(GameplayConfigsProvider configsProvider)
        {
            _dungeonRoomsDatabase = configsProvider.GetConfig<DungeonRoomsDatabase>();
            MapRoomVariantsToTypes();
        }

        public bool TryGetRoom(RoomType type, out DungeonRoom roomData)
        {
            if (_roomDataMap.TryGetValue(type, out var list))
            {
                roomData = list[0];
                return true;
            }

            throw new KeyNotFoundException($"No room data found for type {type}");
        }

        private void MapRoomVariantsToTypes()
        {
            foreach (var roomVariantData in _dungeonRoomsDatabase.Rooms)
            {
                var type = roomVariantData.RoomType;
                
                if(!_roomDataMap.ContainsKey(type))
                    _roomDataMap.Add(type, new ());
                
                var prefab =  roomVariantData.Prefab;
                _roomDataMap[type].Add(prefab.GetComponent<DungeonRoom>());
            }
        }
    }
}