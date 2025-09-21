using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Units;
using Helpers;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.BattleUI.Damage
{
    public class WorldSpaceDamageNumberSpawner : DamageNumberSpawner
    {
        [SerializeField] private Canvas _canvas;
        
        [SerializeField] private float _zSpawnOffet;
        [SerializeField] private float _spawnRadius;

        [Inject]
        private void Construct(CombatService combatService)
        {
            combatService.OnHitDealt.Subscribe(ShowDamageNumber).AddTo(gameObject);
        }
        
        public override void ShowDamageNumber(HitDamageData data)
        {
            if(data.Target is PlayerUnit)
                return;
            
            var view = Pool.Get();

            var enemyPos = data.Target.transform.position;
            var offset = Random.insideUnitSphere * _spawnRadius;
            offset.z = _zSpawnOffet;
            
            var spawnPos = enemyPos + offset;

            view.transform.SetParent(_canvas.transform, false);
            view.transform.position = spawnPos;

            view.SetText(data.Damage);
            Animator.PlayWorldAnimation(view, DamageTypeToColorHelper.GetDamageTypeColor(data.DamageType), Pool);
        }
    }
}