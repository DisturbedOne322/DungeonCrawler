using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Dungeon.Animations
{
    public class ChestAnimator : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _animTime;
        
        public async UniTask PlayOpenAnimation()
        {
            _pivot.transform.DORotate(new Vector3(145, 0, 0), _animTime);
            await UniTask.WaitForSeconds(_animTime);
        }
        
        public void ResetChest() => _pivot.transform.rotation = Quaternion.identity;
    }
}