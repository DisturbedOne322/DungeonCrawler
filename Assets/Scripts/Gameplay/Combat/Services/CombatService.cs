using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Data.Events;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Random = UnityEngine.Random;

namespace Gameplay.Combat.Services
{
    public class CombatService
    {
        private readonly BuffsCalculationService _buffsCalculationService;
        private readonly CombatData _combatData;
        private readonly CombatEventsBus _combatEventsBus;
        private readonly CombatFormulaService _combatFormulaService;

        public CombatService(CombatData combatData, CombatFormulaService combatFormulaService,
            CombatStatusEffectsService combatStatusEffectsService, BuffsCalculationService buffsCalculationService,
            CombatEventsBus combatEventsBus)
        {
            _combatData = combatData;
            _combatFormulaService = combatFormulaService;
            CombatStatusEffectsService = combatStatusEffectsService;
            _buffsCalculationService = buffsCalculationService;
            _combatEventsBus = combatEventsBus;
        }

        public IGameUnit ActiveUnit => _combatData.ActiveUnit;
        public IGameUnit OtherUnit => _combatData.OtherUnit;

        public int TurnCount => _combatData.TurnCount;

        public CombatStatusEffectsService CombatStatusEffectsService { get; }

        public bool IsPlayerTurn() => ActiveUnit is PlayerUnit;

        public void StartCombat(EnemyUnit enemy)
        {
            _combatData.ResetCombat(enemy);
            _combatEventsBus.InvokeCombatStarted(enemy);
        }

        public void EndCombat()
        {
            _combatEventsBus.InvokeCombatEnded(_combatData.Enemy);
        }

        public void StartTurn()
        {
            _combatData.UpdateCurrentTurnUnit();
            _combatEventsBus.InvokeTurnStarted(new TurnData
            {
                TurnCount = TurnCount,
                ActiveUnit = ActiveUnit,
                OtherUnit = OtherUnit,
            });
        }

        public void EndTurn()
        {
            _combatEventsBus.InvokeTurnEnded(new TurnData
            {
                TurnCount = TurnCount,
                ActiveUnit = ActiveUnit
            });
        }

        public void HealActiveUnit(int amount) => HealUnit(ActiveUnit, amount);

        public void DealDamageToActiveUnit(HitDataStream hitDataStream, int index) => DealDamageToUnit(ActiveUnit, ActiveUnit, hitDataStream, index);

        public void DealDamageToActiveUnit(HitData hitData) => DealDamageToUnit(ActiveUnit, ActiveUnit, hitData);
        
        public void DealDamageToOtherUnit(HitDataStream hitDataStream, int index) => DealDamageToUnit(ActiveUnit, OtherUnit, hitDataStream, index);
        public void DealDamageToOtherUnit(HitData hitData) => DealDamageToUnit(ActiveUnit, OtherUnit, hitData);

        public HitDataStream CreateHitsStream(SkillData skillData)
        {
            HitDataStream hitDataStream = new(skillData);

            _buffsCalculationService.ApplyStructuralHitStreamBuffs(ActiveUnit, hitDataStream);
            
            var hits = Random.Range(hitDataStream.MinHits, hitDataStream.MaxHits);
            hitDataStream.CreateHitDataList(hits);

            _buffsCalculationService.BuffHitStream(ActiveUnit, hitDataStream);
            
            return hitDataStream;
        }

        private void DealDamageToUnit(IGameUnit attacker, IGameUnit target, HitDataStream hitDataStream, int index)
        {
            var hitData = hitDataStream.Hits[index];
            DealDamageToUnit(attacker, target, hitData);
            
            if (index == hitDataStream.Hits.Count - 1)
                _combatEventsBus.InvokeSkillUsed(new SkillUsedData
                {
                    Attacker = attacker,
                    Target = target,
                    HitDataStream = hitDataStream
                });
        }
        
        private void DealDamageToUnit(IGameUnit attacker, IGameUnit target, HitData hitData)
        {
            ProcessHitData(attacker, target, hitData);
            
            if (hitData.Missed)
                return;

            target.UnitHealthController.TakeDamage(hitData.Damage);

            _combatEventsBus.InvokeHitDealt(new HitEventData
            {
                Attacker = attacker,
                Target = target,
                HitData = hitData
            });
        }

        private void ProcessHitData(IGameUnit attacker, IGameUnit defender, HitData hitData)
        {
            var damageContext = new DamageContext(attacker, defender, hitData);

            if (Missed(attacker, defender, damageContext))
            {
                hitData.Missed = true;
                defender.EvadeAnimator.PlayEvadeAnimation().Forget();
                _combatEventsBus.InvokeEvaded(new()
                {
                    Attacker = attacker,
                    Target = defender,
                });
                return;
            }

            SetCritical(attacker, damageContext);

            _buffsCalculationService.BuffOutgoingDamage(damageContext);
            _buffsCalculationService.DebuffIncomingDamage(damageContext);

            _combatFormulaService.ApplyFinalDamageMultiplier(damageContext);

            _combatFormulaService.ReduceDamageByStats(defender, damageContext);
        }

        private void SetCritical(IGameUnit caster, in DamageContext damageContext)
        {
            var hitData = damageContext.HitData;
            
            if(hitData.IsCritical != null)
                return;
            
            var finalCritChance = _combatFormulaService.GetFinalCritChance(caster, damageContext);
            hitData.IsCritical = Random.value < finalCritChance;
        }

        private bool Missed(IEntity attacker, IEntity defender, in DamageContext damageContext)
        {
            var hitChance = _combatFormulaService.GetHitChance(attacker, defender, damageContext);
            return Random.value > hitChance;
        }

        private void HealUnit(IGameUnit target, int amount)
        {
            target.UnitHealthController.Heal(amount);

            _combatEventsBus.InvokeHealed(new HealEventData
            { 
                Target = target,
                Amount = amount,
            });
        }
    }
}