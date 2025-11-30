using System.Collections.Generic;
using Data;
using Gameplay.Dungeon.Data;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingSelector
    {
        private readonly PlayerMovementHistory _playerMovementHistory;
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;
        
        public DungeonBranchingSelector(PlayerMovementHistory playerMovementHistory,
            DungeonRoomsProvider dungeonRoomsProvider)
        {
            _playerMovementHistory = playerMovementHistory;
            _dungeonRoomsProvider = dungeonRoomsProvider;
        }

        public List<RoomVariantData> RoomsForSelection { get; } = new(3);

        public void PrepareSelection()
        {
            RoomsForSelection.Clear();

            RoomsForSelection.Add(GetRoomData(RoomType.Combat));
            RoomsForSelection.Add(GetRoomData(RoomType.Shop));
            RoomsForSelection.Add(GetRoomData(RoomType.TreasureChest));
        }
        
        private RoomVariantData GetRoomData(RoomType roomType) => _dungeonRoomsProvider.GetRoomData(roomType);
    }
}