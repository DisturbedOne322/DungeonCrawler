using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Combat.AI;
using UnityEngine;

namespace Gameplay.Units
{
    public class EnemyUnit : GameUnit
    {
        [SerializeField] private Transform _pivot;
        [SerializeField] private float _animTime;
        
        public int ExperienceBonus { get; private set; }
        public AIConfig AIConfig { get; private set; }

        private void OnEnable()
        {
            _pivot.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }

        public override void InitializeUnit(UnitData unitData)
        {
            base.InitializeUnit(unitData);

            if (unitData is EnemyUnitData enemyUnitData)
                AIConfig = enemyUnitData.AIConfig;
        }

        public void SetExperienceBonus(int experienceBonus)
        {
            ExperienceBonus = experienceBonus;
        }

        public async UniTask PlayAppearAnimation()
        {
            _pivot.DORotate(new Vector3(0, 0, 0), _animTime).SetEase(Ease.OutBounce);
            await UniTask.WaitForSeconds(_animTime);
        }
    }
}