using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Modifiers;
using Gameplay.Facades;
using Gameplay.Units;
using UniRx;
using Random = UnityEngine.Random;

namespace Gameplay.Combat
{
    public class CombatService
    {
        private readonly CombatData _combatData;
        private readonly CombatFormulaService _combatFormulaService;
        private readonly CombatBuffsApplicator _combatBuffsApplicator;
        private readonly ModifiersCalculationService _modifiersCalculationService;

        private int _turnCount = -1;
        
        public IGameUnit ActiveUnit => _combatData.ActiveUnit;
        public IGameUnit OtherUnit => _combatData.OtherUnit;
        
        public Subject<HitEventData> OnHitDealt = new();
        public Subject<HealEventData> OnHealed = new();

        public CombatService(CombatData combatData, CombatFormulaService combatFormulaService,
            CombatBuffsApplicator combatBuffsApplicator, ModifiersCalculationService modifiersCalculationService)
        {
            _combatData = combatData;
            _combatFormulaService = combatFormulaService;
            _combatBuffsApplicator = combatBuffsApplicator;
            _modifiersCalculationService = modifiersCalculationService;
        }
        
        public void StartCombat(EnemyUnit enemy)
        {
            _turnCount = -1;
            _combatData.ResetCombat(enemy);
        }

        public void StartTurn()
        {
            _turnCount++;
            _combatData.UpdateCurrentTurnUnit(_turnCount);
            ActiveUnit.UnitBuffsData.Guarded.Value = false;
        }

        public void ApplyGuardToActiveUnit() => _combatBuffsApplicator.ApplyGuardToUnit(ActiveUnit);
        public void ApplyChargeToActiveUnit() => _combatBuffsApplicator.ApplyChargeToUnit(ActiveUnit);
        public void ApplyConcentrateToActiveUnit() => _combatBuffsApplicator.ApplyConcentrateToUnit(ActiveUnit);

        public void HealActiveUnit(int amount)=> HealUnit(ActiveUnit, amount);
        
        public void HealOtherUnit(int amount) => HealUnit(OtherUnit, amount);

        public void DealDamageToActiveUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, ActiveUnit, skillData, consumeCharge);

        public void DealDamageToOtherUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, OtherUnit, skillData, consumeCharge);

        private void DealDamageToUnit(IGameUnit caster, IGameUnit target, OffensiveSkillData skillData, bool consumeCharge = true)
        {
            bool isCritical = IsCritical(caster, skillData);
            
            int outgoingDamage = skillData.Damage;
            var damageContext = new DamageContext(caster, target, skillData, isCritical, consumeCharge);
            outgoingDamage = _modifiersCalculationService.GetFinalOutgoingDamage(outgoingDamage, damageContext);
            
            if (Evaded(target, skillData))
            {
                target.EvadeAnimator.PlayEvadeAnimation();
                return;
            }
            
            outgoingDamage = _modifiersCalculationService.GetReducedIngoingDamage(outgoingDamage, damageContext);
            outgoingDamage = _combatFormulaService.GetFinalDamageTo(outgoingDamage, target, skillData);
            
            target.UnitHealthController.TakeDamage(outgoingDamage);
            
            OnHitDealt.OnNext(new HitEventData()
            {
                Damage = outgoingDamage,
                HitDamageType = isCritical ? HitDamageType.PhysicalCritical : HitDamageType.Physical,
                Target = target
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

        private void HealUnit(IEntity target, int amount)
        {
            target.UnitHealthController.Heal(amount);
            OnHealed?.OnNext(new HealEventData()
            {
                Amount = amount,
                Target = target
            });
        }
    }
}