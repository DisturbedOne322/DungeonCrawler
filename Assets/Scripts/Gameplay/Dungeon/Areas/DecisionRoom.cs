using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using Gameplay.Dungeon.Animations;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Areas
{
    [RequireComponent(typeof(DoorAnimator))]
    public class DecisionRoom : StopRoom
    {
        [SerializeField] private DoorAnimator _doorAnimator;
        
        private DungeonBranchingController _dungeonBranchingController;

        public override RoomType RoomType => RoomType.Decision;
        
        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController) => _dungeonBranchingController = dungeonBranchingController;
        
        public override void ResetRoom() => _doorAnimator.ResetRoom();

        public override async UniTask ClearRoom()
        {
            int doorIndex =  await _dungeonBranchingController.WaitForDecision();
            await _doorAnimator.PlayOpenAnimation(doorIndex);
        }
    }
}