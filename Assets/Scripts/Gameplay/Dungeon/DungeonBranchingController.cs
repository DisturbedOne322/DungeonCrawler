using Cysharp.Threading.Tasks;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonBranchingController
    {
        private readonly PlayerDecisionProvider _decisionProvider;
        private readonly DungeonBranchingSelector _dungeonBranchingSelector;
        private readonly DungeonGenerator _dungeonGenerator;
        private readonly DungeonPositioner _dungeonPositioner;

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
            
            int selectionCount = roomSelection.Count;
            
            var inputIndex = await _decisionProvider.MakeDecision(selectionCount);
            
            var selectedRoom = roomSelection[inputIndex];

            _dungeonPositioner.AddXOffsetFromChoice(inputIndex, selectionCount);

            _dungeonBranchingSelector.PrepareSelection();

            bool lastRoom = _dungeonBranchingSelector.RoomsForSelection.Count == 0;
            
            if(lastRoom)
                _dungeonGenerator.CreateLastMapSection(selectedRoom);
            else
                _dungeonGenerator.CreateNextMapSection(selectedRoom);

            return inputIndex;
        }
    }
}