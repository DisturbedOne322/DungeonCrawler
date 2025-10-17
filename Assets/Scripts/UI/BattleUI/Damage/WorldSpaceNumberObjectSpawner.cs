using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Units;
using Helpers;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.BattleUI.Damage
{
    public class WorldSpaceNumberObjectSpawner : NumberObjectSpawner
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private float _zSpawnOffet;
        [SerializeField] private float _spawnRadius;

        [Inject]
        private void Construct(CombatEventsBus combatEventsBus)
        {
            combatEventsBus.OnHitDealt.Subscribe(ShowDamageNumber).AddTo(gameObject);
        }

        public override void ShowDamageNumber(HitEventData data)
        {
            if (data.Target is PlayerUnit)
                return;

            var enemyPos = data.Target.Position;
            ShowText(enemyPos, data.HitData.Damage, ColorHelpers.GetDamageTypeColor(data), '-');
        }

        public override void ShowHealNumber(HealEventData data)
        {
            if (data.Target is PlayerUnit)
                return;

            var enemyPos = data.Target.Position;
            ShowText(enemyPos, data.Amount, ColorHelpers.GetHealColor(), '+');
        }

        private void ShowText(Vector3 pos, int amount, Color color, char prefix)
        {
            var view = Pool.Get();

            var offset = Random.insideUnitSphere * _spawnRadius;
            offset.z = _zSpawnOffet;

            var spawnPos = pos + offset;

            view.transform.SetParent(_canvas.transform, false);
            view.transform.position = spawnPos;

            view.SetText(amount, prefix);
            Animator.PlayWorldAnimation(view, color, Pool);
        }
    }
}