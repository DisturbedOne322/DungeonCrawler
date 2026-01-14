using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Dungeon.Animations
{
    public class DoorAnimator : MonoBehaviour
    {
        [SerializeField] private List<DecisionDoor> _doorPivots;
        [SerializeField] private float _doorOpenTime;

        public async UniTask PlayOpenAnimation(int doorIndex)
        {
            _doorPivots[doorIndex].transform.DORotate(new Vector3(0, 180, 0), _doorOpenTime).SetLink(gameObject);
            await UniTask.WaitForSeconds(_doorOpenTime);
        }

        public void ResetDoors()
        {
            foreach (var pivot in _doorPivots)
                pivot.transform.rotation = Quaternion.identity;
        }
    }
}