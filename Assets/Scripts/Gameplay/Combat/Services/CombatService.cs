using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
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
            CombatBuffsService combatBuffsService, BuffsCalculationService buffsCalculationService,
            CombatEventsBus combatEventsBus)
        {
            _combatData = combatData;
            _combatFormulaService = combatFormulaService;
            CombatBuffsService = combatBuffsService;
            _buffsCalculationService = buffsCalculationService;
            _combatEventsBus = combatEventsBus;
        }

        public IGameUnit ActiveUnit => _combatData.ActiveUnit;
        public IGameUnit OtherUnit => _combatData.OtherUnit;

        public int TurnCount => _combatData.TurnCount;

        public CombatBuffsService CombatBuffsService { get; }

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
                ActiveUnit = ActiveUnit
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

        public void DealDamageToOtherUnit(HitDataStream hitDataStream, int index) => DealDamageToUnit(ActiveUnit, OtherUnit, hitDataStream, index);

        public HitDataStream CreateFinalHitsStream(SkillData skillData)
        {
            HitDataStream hitDataStream = new(skillData);

            var hits = Random.Range(hitDataStream.MinHits, hitDataStream.MaxHits);
            hitDataStream.CreateHitDataList(hits);

            return hitDataStream;
        }

        private void DealDamageToUnit(IGameUnit attacker, IGameUnit target, HitDataStream hitDataStream, int index)
        {
            ProcessHitData(attacker, target, hitDataStream, index);

            var hit = hitDataStream.Hits[index];

            if (index == hitDataStream.Hits.Count - 1)
                _combatEventsBus.InvokeSkillUsed(new SkillUsedData
                {
                    Attacker = attacker,
                    SkillData = hitDataStream.BaseSkillData
                });

            if (hit.Missed)
                return;

            target.UnitHealthController.TakeDamage(hit.Damage);

            _combatEventsBus.InvokeHitDealt(new HitEventData
            {
                Attacker = attacker,
                Target = target,
                HitData = hit
            });
        }

        private void ProcessHitData(IGameUnit attacker, IGameUnit defender, HitDataStream hitsStream, int hitIndex)
        {
            var hitData = hitsStream.Hits[hitIndex];

            var damageContext = new DamageContext(attacker, defender, hitData, hitsStream.BaseSkillData);

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

            hitData.IsCritical = IsCritical(attacker, damageContext);

            _buffsCalculationService.BuffOutgoingDamage(damageContext);
            _buffsCalculationService.DebuffIncomingDamage(damageContext);

            _combatFormulaService.ApplyFinalDamageMultiplier(damageContext);
            _combatFormulaService.ApplyFinalDefenseMultiplier(damageContext);

            _combatFormulaService.ReduceDamageByStats(defender, damageContext);
        }

        private bool IsCritical(IGameUnit caster, in DamageContext damageContext)
        {
            var finalCritChance = _combatFormulaService.GetFinalCritChance(caster, damageContext);
            return Random.value < finalCritChance;
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