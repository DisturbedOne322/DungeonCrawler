using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Units
{
    public class EnemyUnit : GameUnit
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _animTime;

        private int _experienceBonus;
        public int ExperienceBonus => _experienceBonus;
        
        private void OnEnable()
        {
            _pivot.transform.rotation = Quaternion.Euler(new Vector3(90, 0 ,0));
        }
        public void SetExperienceBonus(int experienceBonus) => _experienceBonus = experienceBonus;
        
        public async UniTask PlayAppearAnimation()
        {
            _pivot.DORotate(new Vector3(0,0,0), _animTime).SetEase(Ease.OutBounce);
            await UniTask.WaitForSeconds(_animTime);
        }
    }
}