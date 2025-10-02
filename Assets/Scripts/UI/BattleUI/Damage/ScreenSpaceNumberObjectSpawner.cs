using Constants;
using Data;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Units;
using Helpers;
using UnityEngine;
using Zenject;
using UniRx;

namespace UI.BattleUI.Damage
{
    public class ScreenSpaceNumberObjectSpawner : NumberObjectSpawner
    {
        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private float _spawnRadius;

        [Inject]
        private void Construct(CombatService combatService)
        {
            combatService.OnHitDealt.Subscribe(ShowDamageNumber).AddTo(gameObject);
            combatService.OnHealed.Subscribe(ShowHealNumber).AddTo(gameObject);
        }
        
        public override void ShowDamageNumber(HitEventData data)
        {
            if(data.Target is not PlayerUnit)
                return;
            
            ShowText(data.Damage, DamageTypeToColorHelper.GetDamageTypeColor(data.HitDamageType), '-');
        }
        
        public override void ShowHealNumber(HealEventData data)
        {
            if(data.Target is not PlayerUnit)
                return;
            
            ShowText(data.Amount, GameplayConstants.HealColor, '+');
        }

        private void ShowText(int amount, Color color, char prefix)
        {
            var view = Pool.Get();
            
            view.transform.SetParent(_spawnPoint, false);
            view.transform.localPosition = Random.insideUnitCircle * _spawnRadius;

            view.SetText(amount, prefix);
            Animator.PlayScreenAnimation(view, color, Pool);
        }
    }
}