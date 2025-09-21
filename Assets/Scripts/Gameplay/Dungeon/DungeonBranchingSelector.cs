using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingSelector
    {
        private readonly PlayerMovementHistory _playerMovementHistory;

        private List<RoomType> _roomsForSelection = new(3);
        
        public List<RoomType> RoomsForSelection => _roomsForSelection;
        
        public DungeonBranchingSelector(PlayerMovementHistory playerMovementHistory)
        {
            _playerMovementHistory = playerMovementHistory;
        }

        public void PrepareSelection()
        {
            _roomsForSelection.Clear();
            
            _roomsForSelection.Add(SelectRandomRoom());
            _roomsForSelection.Add(SelectRandomRoom());
            _roomsForSelection.Add(SelectRandomRoom());
        }

        private RoomType SelectRandomRoom()
        {
            int randomIndex = Random.Range(2, 4);
            return (RoomType)randomIndex;
        }
    }
}