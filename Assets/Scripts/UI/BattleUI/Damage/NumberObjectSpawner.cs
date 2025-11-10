using Gameplay.Combat.Data.Events;
using UnityEngine;
using Zenject;

namespace UI.BattleUI.Damage
{
    public abstract class NumberObjectSpawner : MonoBehaviour
    {
        [SerializeField] private NumberObjectView _prefab;
        [SerializeField] private int _poolSize = 10;
        protected NumberObjectsAnimator Animator;

        protected NumberObjectsPool Pool;

        private void Start()
        {
            Pool.Initialize(_prefab, _poolSize);
        }

        [Inject]
        private void Construct(NumberObjectsPool pool, NumberObjectsAnimator animator)
        {
            Pool = pool;
            Animator = animator;
        }

        public abstract void ShowDamageNumber(HitEventData data);
        public abstract void ShowHealNumber(HealEventData data);
    }
}