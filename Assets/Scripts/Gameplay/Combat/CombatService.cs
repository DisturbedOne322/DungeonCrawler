using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
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
        private readonly CombatBuffsProcessor _combatBuffsProcessor;

        private int _turnCount = -1;
        
        public GameUnit ActiveUnit => _combatData.ActiveUnit;
        public GameUnit OtherUnit => _combatData.OtherUnit;
        
        public Subject<HitEventData> OnHitDealt = new();
        public Subject<HealEventData> OnHealed = new();

        public CombatService(CombatData combatData, CombatFormulaService combatFormulaService,
            CombatBuffsProcessor combatBuffsProcessor)
        {
            _combatData = combatData;
            _combatFormulaService = combatFormulaService;
            _combatBuffsProcessor = combatBuffsProcessor;
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

        public void ApplyGuardToActiveUnit() => _combatBuffsProcessor.ApplyGuardToUnit(ActiveUnit);
        public void ApplyChargeToActiveUnit() => _combatBuffsProcessor.ApplyChargeToUnit(ActiveUnit);
        public void ApplyConcentrateToActiveUnit() => _combatBuffsProcessor.ApplyConcentrateToUnit(ActiveUnit);

        public void HealActiveUnit(int amount)=> HealUnit(ActiveUnit, amount);
        
        public void HealOtherUnit(int amount) => HealUnit(OtherUnit, amount);

        public UniTask DealDamageToActiveUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, ActiveUnit, skillData, consumeCharge);
        
        public UniTask DealDamageToOtherUnit(OffensiveSkillData skillData, bool consumeCharge = true) => 
            DealDamageToUnit(ActiveUnit, OtherUnit, skillData, consumeCharge);

        private async UniTask DealDamageToUnit(GameUnit caster, GameUnit target, OffensiveSkillData skillData, bool consumeCharge = true)
        {
            skillData.Damage = _combatBuffsProcessor.GetBuffedDamage(caster, skillData, consumeCharge);

            if (Evaded(target, skillData))
            {
                await target.EvadeAnimator.PlayEvadeAnimation();
                return;
            }
            
            bool crit = IsCritical(caster, skillData);

            if (crit)
                skillData.Damage *= CritDamageMultiplier;
            
            int damageTaken = _combatFormulaService.GetFinalDamageTo(target, skillData);
            target.UnitHealthController.TakeDamage(damageTaken);
            
            OnHitDealt.OnNext(new HitEventData()
            {
                Damage = damageTaken,
                HitDamageType = crit ? HitDamageType.PhysicalCritical : HitDamageType.Physical,
                Target = target
            });
        }
        
        private bool IsCritical(GameUnit caster, OffensiveSkillData skillData)
        {
            if(!skillData.CanCrit)
                return false;
            
            float finalCritChance = _combatFormulaService.GetFinalCritChance(caster, skillData);
            return Random.value < finalCritChance;
        }

        private bool Evaded(GameUnit unit, OffensiveSkillData skillData)
        {
            float evasionChance = _combatFormulaService.GetFinalEvasionChance(unit, skillData);
            return Random.value < evasionChance;
        }

        private void HealUnit(GameUnit target, int amount)
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