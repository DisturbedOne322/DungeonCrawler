using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingController
    {
        private readonly PlayerDecisionProvider _decisionProvider;
        private readonly DungeonGenerator _dungeonGenerator;

        public DungeonBranchingController(PlayerDecisionProvider decisionProvider, DungeonGenerator dungeonGenerator)
        {
            _decisionProvider = decisionProvider;
            _dungeonGenerator = dungeonGenerator;
        }
        
        public async UniTask WaitForDecision()
        {
            var selectedRoom = await _decisionProvider.MakeDecision(GetRoomSelection());
            _dungeonGenerator.CreateNextMapSection(selectedRoom);
        }
        
        private List<RoomType> GetRoomSelection()
        {
            return new(3)
            {
                RoomType.Corridor,
                RoomType.Corridor,
                RoomType.Corridor,
            };
        }
    }
}