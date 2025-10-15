using AssetManagement.AssetProviders.Core;
using Constants;
using Gameplay.Buffs.Core;
using Gameplay.Configs;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Services
{
    public class CombatFormulaService
    {
        private readonly CombatFormulasConfig _config;

        public CombatFormulaService(BaseConfigProvider<GameplayConfig> gameplayConfigsProvider)
        {
            _config = gameplayConfigsProvider.GetConfig<CombatFormulasConfig>();
        }
        
        public void ReduceDamageByStats(IEntity unit, in DamageContext damageContext)
        {
            int constitutionStat = unit.UnitStatsData.Constitution.Value;
            float damageReductionModifier = 1 - Mathf.Clamp(constitutionStat, 1, StatConstants.MaxStatPoints) 
                * _config.MaxConstitutionInfluence 
                / StatConstants.MaxStatPoints;
            
            float constitutionReducedDamage = damageContext.HitData.Damage * damageReductionModifier;
            
            int clampedDamage = Mathf.Max(Mathf.RoundToInt(constitutionReducedDamage), 0);
            
            damageContext.HitData.Damage = clampedDamage;
        }
        
        public void ApplyFinalDamageMultiplier(in DamageContext damageContext)
        {
            if (damageContext.HitData.IsCritical) 
                ApplyCriticalDamageMultiplier(damageContext);
            
            ApplyAttackMultiplier(damageContext);
        }
        
        public void ApplyFinalDefenseMultiplier(in DamageContext damageContext)
        {
            float defenseMultiplier = damageContext.Defender.UnitBonusStatsData.DefenseMultiplier.Value;

            if (!damageContext.SkillData.IsPiercing)
                defenseMultiplier -= damageContext.SkillData.PenetrationRatio;
            else
                defenseMultiplier = 0;
            
            if(defenseMultiplier < _config.MinDefenseMultiplier)
                defenseMultiplier = _config.MinDefenseMultiplier;
            
            int damage = damageContext.HitData.Damage;
            damageContext.HitData.Damage = Mathf.RoundToInt(damage / defenseMultiplier);
        }
        
        public float GetFinalCritChance(IEntity attacker, in DamageContext damageContext)
        {
            if(!damageContext.SkillData.CanCrit)
                return -1;
            
            float finalCritChance = 0f;
            
            int dex = attacker.UnitStatsData.Dexterity.Value;
            int luck = attacker.UnitStatsData.Luck.Value;

            float chanceFromDex = Mathf.Clamp(dex, 0, StatConstants.MaxStatPoints) * _config.MaxDexterityCritInfluence / StatConstants.MaxStatPoints;
            float chanceFromLuck = Mathf.Clamp(luck, 0, StatConstants.MaxStatPoints) * _config.MaxLuckCritInfluence / StatConstants.MaxStatPoints;

            Debug.Log("cr"+attacker.UnitBonusStatsData.CritChanceBonus.Value);
            
            finalCritChance = damageContext.SkillData.BaseCritChance + attacker.UnitBonusStatsData.CritChanceBonus.Value + chanceFromDex + chanceFromLuck;
            return Mathf.Clamp01(finalCritChance);
        }
        
        public float GetHitChance(IEntity attacker, IEntity defender, in DamageContext damageContext)
        {
            if (damageContext.SkillData.IsUnavoidable)
                return 1;
            
            var hitData = damageContext.HitData;
            float effectiveHitRatio = GetAttackerEffectiveHit(attacker);
            float effectiveEvasion = GetDefenderEffectiveEvasion(defender);
            
            float diff = effectiveHitRatio - effectiveEvasion;
            
            float clamped = Mathf.Clamp(diff, -_config.MaxEffectiveStatDiff, _config.MaxEffectiveStatDiff);
            
            float slope = _config.MaxStatHitInfluence / _config.MaxEffectiveStatDiff;
            float statHit = _config.EqualStatsHitChance + clamped * slope;
            
            statHit = Mathf.Clamp01(statHit);
            
            float finalHitChance = statHit * Mathf.Clamp01(hitData.HitChance);

            return Mathf.Clamp01(finalHitChance);
        }
        
        private float GetAttackerEffectiveHit(IEntity unit)
        {
            var stats = unit.UnitStatsData;
            float effectiveDex = stats.Dexterity.Value * _config.AttackerDexterityInfluence;
            float effectiveLuck = stats.Luck.Value * _config.AttackerLuckInfluence;
            return effectiveDex + effectiveLuck;
        }
        
        private float GetDefenderEffectiveEvasion(IEntity unit)
        {
            var stats = unit.UnitStatsData;
            float effectiveDex = stats.Dexterity.Value * _config.DefenderDexterityInfluence;
            float effectiveLuck = stats.Luck.Value * _config.DefenderLuckInfluence;
            return effectiveDex + effectiveLuck;
        }
        
        private void ApplyCriticalDamageMultiplier(in DamageContext damageContext)
        {
            float damageMultiplier = GetCriticalDamageMultiplier(damageContext);
            int damage = damageContext.HitData.Damage;
            damageContext.HitData.Damage = Mathf.RoundToInt(damage * damageMultiplier);
        }
        
        private void ApplyAttackMultiplier(in DamageContext damageContext)
        {
            float damageMultiplier = damageContext.Attacker.UnitBonusStatsData.AttackMultiplier.Value;
            int damage = damageContext.HitData.Damage;
            damageContext.HitData.Damage = Mathf.RoundToInt(damage * damageMultiplier);
        }
        
        private float GetCriticalDamageMultiplier(in DamageContext damageContext)
        {
            return GameplayConstants.CriticalDamageMultiplier +
                   damageContext.Attacker.UnitBonusStatsData.CritDamageBonus.Value;
        }
    }
}