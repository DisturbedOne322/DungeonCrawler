using System;
using Gameplay.Combat.Data;
using UnityEngine;
using Zenject;

namespace UI.BattleUI.Damage
{
    public abstract class NumberObjectSpawner : MonoBehaviour
    {
        [SerializeField] private NumberObjectView _prefab;
        
        protected NumberObjectsPool Pool;
        protected NumberObjectsAnimator Animator;

        [Inject]
        private void Construct(NumberObjectsPool pool, NumberObjectsAnimator animator)
        {
            Pool = pool;
            Animator = animator;
        }

        private void Start() => Pool.Initialize(_prefab);

        public abstract void ShowDamageNumber(HitEventData data);
        public abstract void ShowHealNumber(HealEventData data);
    }
}