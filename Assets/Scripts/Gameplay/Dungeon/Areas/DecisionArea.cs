using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Areas
{
    public class DecisionArea : StopArea
    {
        [SerializeField] private Transform _door;
        [SerializeField] private float _doorOpenTime;
        
        private DungeonBranchingController _dungeonBranchingController;

        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController)
        {
            _dungeonBranchingController = dungeonBranchingController;
        }
        
        public override RoomType RoomType => RoomType.Decision;

        public override async UniTask ClearRoom()
        {
            await _dungeonBranchingController.WaitForDecision();
            
            _door.DORotate(new Vector3(0, 90, 0), _doorOpenTime);
            
            await UniTask.WaitForSeconds(_doorOpenTime);
        }
    }
}