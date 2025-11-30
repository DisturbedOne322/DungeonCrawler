using System.Collections.Generic;
using System.Linq;
using AssetManagement.AssetProviders.ConfigProviders;
using Data;
using Gameplay.Dungeon.Data;

namespace Gameplay.Dungeon
{
    public class DungeonRoomsProvider
    {
        private readonly DungeonRoomsDatabase _dungeonRoomsDatabase;
        private readonly PlayerMovementHistory _playerMovementHistory;
        
        private readonly Dictionary<RoomType, List<RoomVariantData>> _roomDataMap = new();

        public DungeonRoomsProvider(GameplayConfigsProvider configsProvider,
            PlayerMovementHistory playerMovementHistory)
        {
            _playerMovementHistory = playerMovementHistory;
            _dungeonRoomsDatabase = configsProvider.GetConfig<DungeonRoomsDatabase>();
            MapRoomVariantsToTypes();
        }

        public List<RoomVariantData> GetRoomsSelection()
        {
            int currentDepth = _playerMovementHistory.Depth;
            
            List<RoomVariantData> selection = new();

            var roomsPerType = _roomDataMap.Values.ToList();
            
            foreach (var roomsList in roomsPerType)
            {
                foreach (var roomData in roomsList)
                    if(IsValidRoomForSelection(roomData, currentDepth))
                        selection.Add(roomData);
            }
            
            return selection;
        }

        public RoomVariantData GetDecisionRoomData()
        {
            int currentDepth = _playerMovementHistory.Depth;
            
            var decisionRooms = _roomDataMap[RoomType.Decision];
            
            foreach (var decisionRoomData in decisionRooms)
                if (IsValidDepth(decisionRoomData, currentDepth))
                    return decisionRoomData;
            
            throw new KeyNotFoundException($"No acceptable room data found for type {RoomType.Decision}");
        }
        
        public RoomVariantData GetCorridorRoomData()
        {
            int currentDepth = _playerMovementHistory.Depth;
            
            var corridorRooms = _roomDataMap[RoomType.Corridor];
            
            foreach (var corridorRoomData in corridorRooms)
                if (IsValidDepth(corridorRoomData, currentDepth))
                    return corridorRoomData;
            
            throw new KeyNotFoundException($"No acceptable room data found for type {RoomType.Corridor}");
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

        private bool IsValidRoomForSelection(RoomVariantData roomData, int depth)
        {
            if(!IsValidRoomTypeForSelection(roomData.RoomType))
                return false;
            
            return IsValidDepth(roomData, depth);
        }

        private bool IsValidDepth(RoomVariantData roomData, int depth)
        {
            if(depth < roomData.MinDepth)
                return false;
            
            if(depth > roomData.MaxDepth)
                return false;
            
            return true;
        }

        private bool IsValidRoomTypeForSelection(RoomType roomType)
        {
            return roomType is not (RoomType.Corridor or RoomType.Decision);
        }
    }
}