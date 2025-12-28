using System.Collections.Generic;
using System.Linq;
using AssetManagement.AssetProviders.ConfigProviders;
using Data;
using Gameplay.Dungeon.RoomVariants;

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

        public RoomVariantData GetDecisionRoomData() =>GetRoomData(RoomType.Decision);
        
        public RoomVariantData GetCorridorRoomData() => GetRoomData(RoomType.Corridor);

        public TrapRoomVariantData GetTrapRoomData() => GetRoomData(RoomType.Trap) as TrapRoomVariantData;

        private RoomVariantData GetRoomData(RoomType roomType)
        {
            int currentDepth = _playerMovementHistory.Depth;
            
            var rooms = _roomDataMap[roomType];
            
            foreach (var roomData in rooms)
                if (IsValidDepth(roomData, currentDepth))
                    return roomData;
            
            throw new KeyNotFoundException($"No acceptable room data found for type {roomType}");
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
            return roomType is not (RoomType.Corridor or RoomType.Decision or RoomType.Trap);
        }
    }
}