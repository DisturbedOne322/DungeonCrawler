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
        private readonly DungeonPositioner _dungeonPositioner;

        public DungeonBranchingController(PlayerDecisionProvider decisionProvider, 
            DungeonGenerator dungeonGenerator, DungeonPositioner dungeonPositioner)
        {
            _decisionProvider = decisionProvider;
            _dungeonGenerator = dungeonGenerator;
            _dungeonPositioner = dungeonPositioner;
        }
        
        public async UniTask<int> WaitForDecision()
        {
            var roomSelection = GetRoomSelection();
            
            var inputIndex = await _decisionProvider.MakeDecision();
            var selectedRoom = roomSelection[inputIndex];
            
            _dungeonPositioner.AddXOffsetFromChoice(inputIndex);
            _dungeonGenerator.CreateNextMapSection(selectedRoom);
            
            return inputIndex;
        }
        
        private List<RoomType> GetRoomSelection()
        {
            return new(3)
            {
                RoomType.TreasureChest,
                RoomType.Corridor,
                RoomType.Corridor,
            };
        }
    }
}