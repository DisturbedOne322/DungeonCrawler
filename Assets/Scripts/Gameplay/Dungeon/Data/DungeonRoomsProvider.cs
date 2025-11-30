using System.Collections.Generic;
using AssetManagement.AssetProviders.ConfigProviders;
using Data;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    public class DungeonRoomsProvider
    {
        private readonly DungeonRoomsDatabase _dungeonRoomsDatabase;
        
        private readonly Dictionary<RoomType, List<RoomVariantData>> _roomDataMap = new();

        public DungeonRoomsProvider(GameplayConfigsProvider configsProvider)
        {
            _dungeonRoomsDatabase = configsProvider.GetConfig<DungeonRoomsDatabase>();
            MapRoomVariantsToTypes();
        }

        public List<RoomVariantData> GetRoomsSelection(RoomType roomType)
        {
            if (!_roomDataMap.TryGetValue(roomType, out var selection))
            {
                Debug.LogError("No room variants  found for type " + roomType);
                return null;
            }
            
            return selection;
        }
        
        public RoomVariantData GetRoomData(RoomType type)
        {
            if (_roomDataMap.TryGetValue(type, out var list))
                return list[0];
            
            throw new KeyNotFoundException($"No room data found for type {type}");
        }

        private void MapRoomVariantsToTypes()
        {
            foreach (var roomVariantData in _dungeonRoomsDatabase.Rooms)
            {
                var type = roomVariantData.RoomType;
                
                if(!_roomDataMap.ContainsKey(type))
                    _roomDataMap.Add(type, new ());
                
                _roomDataMap[type].Add(roomVariantData);
            }
        }
    }
}