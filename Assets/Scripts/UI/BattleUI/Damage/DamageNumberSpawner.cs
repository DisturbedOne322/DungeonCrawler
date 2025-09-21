using System;
using Gameplay.Combat.Data;
using UnityEngine;
using Zenject;

namespace UI.BattleUI.Damage
{
    public abstract class DamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private DamageNumberView _prefab;
        
        protected DamageNumbersPool Pool;
        protected DamageNumbersAnimator Animator;

        [Inject]
        private void Construct(DamageNumbersPool pool, DamageNumbersAnimator animator)
        {
            Pool = pool;
            Animator = animator;
        }

        private void Start()
        {
            Pool.Initialize(_prefab);
        }

        public abstract void ShowDamageNumber(HitDamageData data);
    }
}