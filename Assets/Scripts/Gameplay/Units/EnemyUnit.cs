using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Combat;
using UnityEngine;

namespace Gameplay
{
    public class EnemyUnit : GameUnit
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _animTime;

        private void OnEnable()
        {
            _pivot.transform.rotation = Quaternion.Euler(new Vector3(90, 0 ,0));
        }

        public async UniTask PlayAppearAnimation()
        {
            _pivot.DORotate(new Vector3(0,0,0), _animTime).SetEase(Ease.OutBounce);
            await UniTask.WaitForSeconds(_animTime);
        }
    }
}