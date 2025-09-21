using Cysharp.Threading.Tasks;
using Gameplay.Player;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingController
    {
        private readonly PlayerDecisionProvider _decisionProvider;
        private readonly DungeonGenerator _dungeonGenerator;
        private readonly DungeonPositioner _dungeonPositioner;
        private readonly DungeonBranchingSelector _dungeonBranchingSelector;

        public DungeonBranchingController(PlayerDecisionProvider decisionProvider, 
            DungeonGenerator dungeonGenerator, DungeonPositioner dungeonPositioner, 
            DungeonBranchingSelector dungeonBranchingSelector)
        {
            _decisionProvider = decisionProvider;
            _dungeonGenerator = dungeonGenerator;
            _dungeonPositioner = dungeonPositioner;
            _dungeonBranchingSelector = dungeonBranchingSelector;
        }
        
        public async UniTask<int> WaitForDecision()
        {
            var roomSelection = _dungeonBranchingSelector.RoomsForSelection;
            
            var inputIndex = await _decisionProvider.MakeDecision();
            var selectedRoom = roomSelection[inputIndex];
            
            _dungeonPositioner.AddXOffsetFromChoice(inputIndex);
            
            _dungeonBranchingSelector.PrepareSelection();
            _dungeonGenerator.CreateNextMapSection(selectedRoom);
            
            return inputIndex;
        }
    }
}