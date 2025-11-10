using Gameplay.Combat.Data.Events;
using Gameplay.Combat.Services;
using Gameplay.Units;
using Helpers;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.BattleUI.Damage
{
    public class ScreenSpaceNumberObjectSpawner : NumberObjectSpawner
    {
        [SerializeField] private RectTransform _spawnPoint;
        [SerializeField] private float _spawnRadius;

        [Inject]
        private void Construct(CombatEventsBus combatEventsBus)
        {
            combatEventsBus.OnHitDealt.Subscribe(ShowDamageNumber).AddTo(gameObject);
            combatEventsBus.OnHealed.Subscribe(ShowHealNumber).AddTo(gameObject);
        }

        public override void ShowDamageNumber(HitEventData data)
        {
            if (data.Target is not PlayerUnit)
                return;

            ShowText(data.HitData.Damage, ColorHelpers.GetDamageTypeColor(data), '-');
        }

        public override void ShowHealNumber(HealEventData data)
        {
            if (data.Target is not PlayerUnit)
                return;

            ShowText(data.Amount, ColorHelpers.GetHealColor(), '+');
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