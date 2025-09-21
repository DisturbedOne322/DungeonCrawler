using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Units;
using Helpers;
using UnityEngine;
using Zenject;
using UniRx;

namespace UI.BattleUI.Damage
{
    public class ScreenSpaceDamageNumberSpawner : DamageNumberSpawner
    {
        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private float _spawnRadius;

        [Inject]
        private void Construct(CombatService combatService)
        {
            combatService.OnHitDealt.Subscribe(ShowDamageNumber).AddTo(gameObject);
        }
        
        public override void ShowDamageNumber(HitDamageData data)
        {
            if(data.Target is not PlayerUnit)
                return;
            
            var view = Pool.Get();
            
            view.transform.SetParent(_spawnPoint, false);
            view.transform.localPosition = Random.insideUnitCircle * _spawnRadius;

            view.SetText(data.Damage);
            Animator.PlayScreenAnimation(view, DamageTypeToColorHelper.GetDamageTypeColor(data.DamageType), Pool);
        }
    }
}