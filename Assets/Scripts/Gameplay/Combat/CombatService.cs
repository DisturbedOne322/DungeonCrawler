using Cysharp.Threading.Tasks;
using Gameplay.Buffs;
using Gameplay.Buffs.Core;
using Gameplay.Buffs.Services;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Combat
{
    public class CombatService
    {
        private readonly CombatData _combatData;
        private readonly CombatFormulaService _combatFormulaService;
        private readonly CombatBuffsService _combatBuffsService;
        private readonly BuffsCalculationService _buffsCalculationService;
        private readonly CombatEventsService _combatEventsService;
        
        public IGameUnit ActiveUnit => _combatData.ActiveUnit;
        public IGameUnit OtherUnit => _combatData.OtherUnit;
        
        public int TurnCount => _combatData.TurnCount;

        public CombatService(CombatData combatData, CombatFormulaService combatFormulaService,
            CombatBuffsService combatBuffsService, BuffsCalculationService buffsCalculationService,
            CombatEventsService combatEventsService)
        {
            _combatData = combatData;
            _combatFormulaService = combatFormulaService;
            _combatBuffsService = combatBuffsService;
            _buffsCalculationService = buffsCalculationService;
            _combatEventsService = combatEventsService;
        }

        public CombatBuffsService CombatBuffsService => _combatBuffsService;

        public bool IsPlayerTurn() => ActiveUnit is PlayerUnit;
        
        public void StartCombat(EnemyUnit enemy)
        {
            _combatData.ResetCombat(enemy);
            _combatEventsService.InvokeCombatStarted(enemy);
        }

        public void EndCombat()
        {
            _combatEventsService.InvokeCombatEnded(_combatData.Enemy);
        }

        public void StartTurn()
        {
            _combatData.UpdateCurrentTurnUnit();
            _combatEventsService.InvokeTurnStarted(new ()
            {
                TurnCount = TurnCount,
                ActiveUnit = ActiveUnit
            });
        }

        public void EndTurn()
        {
            _combatEventsService.InvokeTurnEnded(new()
            {
                TurnCount = TurnCount,
                ActiveUnit = ActiveUnit
            });
        }
        
        public void HealActiveUnit(int amount) => HealUnit(ActiveUnit, amount);
        
        public void HealOtherUnit(int amount) => HealUnit(OtherUnit, amount);

        public void DealDamageToActiveUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, ActiveUnit, skillData, consumeCharge);

        public void DealDamageToOtherUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, OtherUnit, skillData, consumeCharge);

        private void DealDamageToUnit(IGameUnit caster, IGameUnit target, 
            OffensiveSkillData skillData, bool consumeCharge = true)
        {
            bool isCritical = IsCritical(caster, skillData);
            
            int outgoingDamage = skillData.Damage;
            var damageContext = new DamageContext(caster, target, skillData, isCritical, consumeCharge);
            outgoingDamage = _buffsCalculationService.GetFinalOutgoingDamage(outgoingDamage, damageContext);
            
            if (Evaded(target, skillData))
            {
                target.EvadeAnimator.PlayEvadeAnimation().Forget();
                _combatEventsService.InvokeEvaded(target);
                return;
            }
            
            outgoingDamage = _buffsCalculationService.GetReducedIngoingDamage(outgoingDamage, damageContext);
            outgoingDamage = _combatFormulaService.GetFinalDamageTo(outgoingDamage, target, skillData);
            
            target.UnitHealthController.TakeDamage(outgoingDamage);
            
            _combatEventsService.InvokeHitDealt(new HitEventData()
            {
                Attacker = caster,
                Target = target,
                IsCritical = isCritical,
                Damage = outgoingDamage,
                SkillData = skillData,
            });
        }
        
        private bool IsCritical(IGameUnit caster, OffensiveSkillData skillData)
        {
            if(!skillData.CanCrit)
                return false;
            
            float finalCritChance = _combatFormulaService.GetFinalCritChance(caster, skillData);
            return Random.value < finalCritChance;
        }

        private bool Evaded(IEntity unit, OffensiveSkillData skillData)
        {
            float evasionChance = _combatFormulaService.GetFinalEvasionChance(unit, skillData);
            return Random.value < evasionChance;
        }

        private void HealUnit(IGameUnit target, int amount)
        {
            target.UnitHealthController.Heal(amount);
            
            _combatEventsService.InvokeHealed(new HealEventData()
            {
                Amount = amount,
                Target = target
            });
        }
    }
}