using System.Collections.Generic;
using System.Linq;
using AssetManagement.AssetProviders.ConfigProviders;
using Data;
using Gameplay.Dungeon.RoomVariants;
using Helpers;

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
            var currentDepth = _playerMovementHistory.Depth;

            List<RoomVariantData> selection = new();

            var roomsPerType = _roomDataMap.Values.ToList();

            foreach (var roomsList in roomsPerType)
            foreach (var roomData in roomsList)
                if (IsValidRoomForSelection(roomData, currentDepth))
                    selection.Add(roomData);

            return selection;
        }

        public RoomVariantData GetRoomData(RoomType roomType)
        {
            var currentDepth = _playerMovementHistory.Depth;

            var rooms = _roomDataMap[roomType];

            foreach (var roomData in rooms)
                if (IsValidDepth(roomData, currentDepth))
                    return roomData;

            return null;
        }

        public DecisionRoomVariantData GetDecisionRoomData(int selection)
        {
            var currentDepth = _playerMovementHistory.Depth;

            var decisionRooms = _roomDataMap[RoomType.Decision];

            foreach (var decisionRoom in decisionRooms)
            {
                if(!IsValidDepth(decisionRoom, currentDepth))
                    continue;
                
                var room = decisionRoom as DecisionRoomVariantData;
                if(room.RoomsForSelection == selection)
                    return room;
            }
            
            return null;
        }

        private void MapRoomVariantsToTypes()
        {
            foreach (var roomVariantData in _dungeonRoomsDatabase.Rooms)
            {
                var type = roomVariantData.RoomType;

                if (!_roomDataMap.ContainsKey(type))
                    _roomDataMap.Add(type, new List<RoomVariantData>());

                _roomDataMap[type].Add(roomVariantData);
            }
        }

        private bool IsValidRoomForSelection(RoomVariantData roomData, int depth)
        {
            if (!roomData)
                return false;

            if (!RoomTypeHelper.IsRoomValidForSelection(roomData.RoomType))
                return false;

            return IsValidDepth(roomData, depth);
        }

        private bool IsValidDepth(RoomVariantData roomData, int depth)
        {
            if (depth < roomData.MinDepth)
                return false;

            if (depth > roomData.MaxDepth)
                return false;

            return true;
        }
    }
}