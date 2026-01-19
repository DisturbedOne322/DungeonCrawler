using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Data.Events;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.Units;
using Helpers;
using UnityEngine;

namespace Gameplay.Combat.Services
{
    public class DamageDealingService
    {
        private readonly CombatEventsBus _combatEventsBus;
        private readonly CombatFormulaService _combatFormulaService;
        private readonly PlayerUnit _playerUnit;

        public DamageDealingService(CombatFormulaService combatFormulaService, 
            CombatEventsBus combatEventsBus, PlayerUnit playerUnit)
        {
            _combatFormulaService = combatFormulaService;
            _combatEventsBus = combatEventsBus;
            _playerUnit = playerUnit;
        }

        public void DealDamageToPlayer(ICombatant damageSource, HitData hitData)
        {
            DealDamageToUnit(damageSource, _playerUnit, hitData);
        }

        public void DealDamageToUnit(ICombatant attacker, IGameUnit target, HitData hitData)
        {
            ProcessHit(attacker, target, hitData);

            if (hitData.Missed)
            {
                target.EvadeAnimator.PlayEvadeAnimation().Forget();
                _combatEventsBus.InvokeEvaded(new EvadeEventData
                {
                    Attacker = attacker,
                    Target = target
                });
                return;
            }

            var hpPercentBeforeHit = HealthHelper.GetHealthPercent(target);

            target.UnitHealthController.TakeDamage(hitData.Damage);

            _combatEventsBus.InvokeHitDealt(new HitEventData
            {
                HealthPercentBeforeHit = hpPercentBeforeHit,
                Attacker = attacker,
                Target = target,
                HitData = hitData
            });
        }

        public void ProcessHit(ICombatant attacker, IGameUnit defender, HitData hitData)
        {
            var damageContext = new DamageContext(attacker, defender, hitData);
            ProcessDamage(damageContext);
            SetMissed(attacker, defender, damageContext);
        }

        private void ProcessDamage(DamageContext damageContext)
        {
            SetCritical(damageContext);

            BuffApplicationService.BuffOutgoingHits(damageContext);
            BuffApplicationService.DebuffIncomingHits(damageContext);

            _combatFormulaService.ApplyFinalDamageMultiplier(damageContext);
            _combatFormulaService.ReduceDamageByStats(damageContext);
        }

        private void SetCritical(in DamageContext damageContext)
        {
            var hitData = damageContext.HitData;

            if (hitData.IsCritical)
                return;

            var finalCritChance = _combatFormulaService.GetFinalCritChance(damageContext.Attacker, damageContext);
            hitData.IsCritical = Random.value < finalCritChance;
        }

        private void SetMissed(ICombatant attacker, ICombatant defender, in DamageContext damageContext)
        {
            var hitData = damageContext.HitData;
            var hitChance = _combatFormulaService.GetHitChance(attacker, defender, damageContext);

            hitData.Missed = Random.value > hitChance;
        }
    }
}