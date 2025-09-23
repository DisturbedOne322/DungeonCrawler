using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon.Animations;
using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    [RequireComponent(typeof(DoorAnimator))]
    public class DecisionRoom : StopRoom
    {
        [SerializeField] private DoorAnimator _doorAnimator;
        [SerializeField] private List<DecisionDoor> _doors;

        private DungeonBranchingController _dungeonBranchingController;
        private DungeonFactory _dungeonFactory;
        private DungeonBranchingSelector _dungeonBranchingSelector;

        public override RoomType RoomType => RoomType.Decision;
        
        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController, 
            DungeonFactory dungeonFactory, 
            DungeonBranchingSelector dungeonBranchingSelector)
        {
            _dungeonBranchingController = dungeonBranchingController;
            _dungeonFactory = dungeonFactory;
            _dungeonBranchingSelector = dungeonBranchingSelector;
        }

        public override void ResetRoom()
        {
            _doorAnimator.ResetDoors();
        }

        public override void SetupRoom()
        {
            var doorTypes = _dungeonBranchingSelector.RoomsForSelection;
            for (int i = 0; i < doorTypes.Count; i++)
            {
                var door = _doors[i];
                door.SetDoorIcon(_dungeonFactory.GetRoomData(doorTypes[i]).Icon);
            } 
        }

        public override async UniTask ClearRoom()
        {
            int doorIndex =  await _dungeonBranchingController.WaitForDecision();
            await _doorAnimator.PlayOpenAnimation(doorIndex);
        }
    }
}