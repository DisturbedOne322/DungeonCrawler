using System;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Units;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Combat
{
    public class CombatService
    {
        private const int CritDamageMultiplier = 2;
        
        private readonly CombatData _combatData;
        private readonly CombatFormulaService _combatFormulaService;

        private int _turnCount = -1;
        
        public GameUnit ActiveUnit => _combatData.ActiveUnit;
        public GameUnit OtherUnit => _combatData.OtherUnit;
        
        public Subject<HitDamageData> OnHitDealt = new();

        public CombatService(CombatData combatData, CombatFormulaService combatFormulaService)
        {
            _combatData = combatData;
            _combatFormulaService = combatFormulaService;
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

        public void ApplyGuardToActiveUnit() => ActiveUnit.UnitBuffsData.Guarded.Value = true;
        public void ApplyChargeToActiveUnit() => ActiveUnit.UnitBuffsData.Charged.Value = true;
        public void ApplyConcentrateToActiveUnit() => ActiveUnit.UnitBuffsData.Concentrated.Value = true;
        
        public void DealDamageToActiveUnit(OffensiveSkillData skillData)=> DealDamageToUnit(ActiveUnit, ActiveUnit, skillData);
        
        public void DealDamageToOtherUnit(OffensiveSkillData skillData) => DealDamageToUnit(ActiveUnit, OtherUnit, skillData);

        private void DealDamageToUnit(GameUnit caster, GameUnit target, OffensiveSkillData skillData)
        {
            if (Evaded(target, skillData))
            {
                target.EvadeAnimator.PlayEvadeAnimation(0).Forget();
                return;
            }
            
            if (caster.UnitBuffsData.Charged.Value)
            {
                caster.UnitBuffsData.Charged.Value = false;
                skillData.Damage *= 2;
            }
            
            bool crit = IsCritical(caster, skillData);

            if (crit)
                skillData.Damage *= CritDamageMultiplier;
            
            int damageTaken = _combatFormulaService.GetFinalDamageTo(target, skillData);
            target.UnitHealthController.TakeDamage(damageTaken);
            
            OnHitDealt.OnNext(new HitDamageData()
            {
                Damage = damageTaken,
                DamageType = crit ? DamageType.PhysicalCritical : DamageType.Physical,
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
    }
}