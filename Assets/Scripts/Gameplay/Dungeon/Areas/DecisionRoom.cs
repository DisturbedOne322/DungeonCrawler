using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Areas
{
    public class DecisionRoom : StopRoom
    {
        [SerializeField] private Transform _doorPivot;
        [SerializeField] private float _doorOpenTime;
        
        private DungeonBranchingController _dungeonBranchingController;

        [Inject]
        private void Construct(DungeonBranchingController dungeonBranchingController) => _dungeonBranchingController = dungeonBranchingController;

        public override RoomType RoomType => RoomType.Decision;
        
        public override void ResetRoom() => _doorPivot.transform.rotation = Quaternion.identity;

        public override async UniTask ClearRoom()
        {
            await _dungeonBranchingController.WaitForDecision();
            
            _doorPivot.DORotate(new Vector3(0, 90, 0), _doorOpenTime);
            
            await UniTask.WaitForSeconds(_doorOpenTime);
        }
    }
}