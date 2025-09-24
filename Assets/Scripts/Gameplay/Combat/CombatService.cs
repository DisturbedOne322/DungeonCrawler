using System;
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
        
        private readonly Subject<HitDamageData> _onHitDealt = new();
        
        public IObservable<HitDamageData> OnHitDealt => _onHitDealt;

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
        
        public void DealDamageToActiveUnit(OffensiveSkillData skillData)
        {
            int damageTaken = _combatFormulaService.GetFinalDamageTo(ActiveUnit, skillData);
            ActiveUnit.UnitHealthController.TakeDamage(damageTaken);
            
            _onHitDealt.OnNext(new HitDamageData()
            {
                Damage = damageTaken,
                DamageType = DamageType.Physical,
                Target = ActiveUnit
            });
        }
        
        public void DealDamageToOtherUnit(OffensiveSkillData skillData)
        {
            float finalCritChance = _combatFormulaService.GetFinalCritChance(ActiveUnit, skillData);

            bool crit = IsCrit(finalCritChance);
            
            if (crit)
                skillData.Damage *= CritDamageMultiplier;
            
            if (ActiveUnit.UnitBuffsData.Charged.Value)
            {
                ActiveUnit.UnitBuffsData.Charged.Value = false;
                skillData.Damage *= 2;
            }
            
            int damageTaken = _combatFormulaService.GetFinalDamageTo(OtherUnit, skillData);
            OtherUnit.UnitHealthController.TakeDamage(damageTaken);
            
            _onHitDealt.OnNext(new HitDamageData()
            {
                Damage = damageTaken,
                DamageType = crit ? DamageType.PhysicalCritical : DamageType.Physical,
                Target = OtherUnit
            });
        }
        
        private bool IsCrit(float chance) => Random.value < chance;
    }
}