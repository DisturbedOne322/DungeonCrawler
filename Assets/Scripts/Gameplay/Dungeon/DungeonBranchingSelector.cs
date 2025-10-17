using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingSelector
    {
        private readonly PlayerMovementHistory _playerMovementHistory;

        public DungeonBranchingSelector(PlayerMovementHistory playerMovementHistory)
        {
            _playerMovementHistory = playerMovementHistory;
        }

        public List<RoomType> RoomsForSelection { get; } = new(3);

        public void PrepareSelection()
        {
            RoomsForSelection.Clear();

            RoomsForSelection.Add(SelectRandomRoom());
            RoomsForSelection.Add(SelectRandomRoom());
            RoomsForSelection.Add(SelectRandomRoom());
        }

        private RoomType SelectRandomRoom()
        {
            var randomIndex = Random.Range(2, 4);
            return (RoomType)randomIndex;
        }
    }
}