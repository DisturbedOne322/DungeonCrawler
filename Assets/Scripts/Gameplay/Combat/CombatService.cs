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
        private const int CritDamageMultiplier = 2;
        
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
        
        public void StartCombat(GameUnit enemy)
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

        public UniTask DealDamageToActiveUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, ActiveUnit, skillData, consumeCharge);
        
        public UniTask DealDamageToOtherUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, OtherUnit, skillData, consumeCharge);

        private async UniTask DealDamageToUnit(IGameUnit caster, IGameUnit target, OffensiveSkillData skillData, bool consumeCharge = true)
        {
            bool isCritical = IsCritical(caster, skillData);

            var damageContext = new DamageContext(caster, target, skillData, isCritical, consumeCharge);
            skillData.Damage = _modifiersCalculationService.GetFinalOutgoingDamage(damageContext);

            if (Evaded(target, skillData))
            {
                await target.EvadeAnimator.PlayEvadeAnimation();
                return;
            }
            
            skillData.Damage = _modifiersCalculationService.GetReducedIngoingDamage(damageContext);
            int finalDamage = _combatFormulaService.GetFinalDamageTo(target, skillData);
            
            target.UnitHealthController.TakeDamage(finalDamage);
            
            OnHitDealt.OnNext(new HitEventData()
            {
                Damage = finalDamage,
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