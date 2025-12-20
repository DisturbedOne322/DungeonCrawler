using AssetManagement.AssetProviders;
using AssetManagement.AssetProviders.ConfigProviders;
using Data.Constants;
using Gameplay.Configs;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Services
{
    public class CombatFormulaService
    {
        private readonly CombatFormulasConfig _config;

        public CombatFormulaService(GameplayConfigsProvider gameplayConfigsProvider)
        {
            _config = gameplayConfigsProvider.GetConfig<CombatFormulasConfig>();
        }

        public void ReduceDamageByStats(in DamageContext damageContext)
        {
            var damageReductionModifier = GetDamageReductionFinal(damageContext);

            var reducedDamage = damageContext.HitData.Damage * damageReductionModifier;
            var clampedDamage = Mathf.Max(Mathf.RoundToInt(reducedDamage), 0);

            damageContext.HitData.Damage = clampedDamage;
        }

        public void ApplyFinalDamageMultiplier(in DamageContext damageContext)
        {
            if (damageContext.HitData.IsCritical)
                ApplyCriticalDamageMultiplier(damageContext);
        }

        public float GetFinalCritChance(IEntity attacker, in DamageContext damageContext)
        {
            if (!damageContext.HitData.CanCrit)
                return -1;

            var finalCritChance = 0f;

            var dex = attacker.UnitStatsData.Dexterity.Value;
            var luck = attacker.UnitStatsData.Luck.Value;

            var chanceFromDex = Mathf.Clamp(dex, 0, StatConstants.MaxStatPoints) * _config.MaxDexterityCritInfluence /
                                StatConstants.MaxStatPoints;
            var chanceFromLuck = Mathf.Clamp(luck, 0, StatConstants.MaxStatPoints) * _config.MaxLuckCritInfluence /
                                 StatConstants.MaxStatPoints;

            finalCritChance = damageContext.HitData.CritChance +
                              attacker.UnitBonusStatsData.CritChanceBonus.Value + chanceFromDex + chanceFromLuck;
            return Mathf.Clamp01(finalCritChance);
        }

        public float GetHitChance(IEntity attacker, IEntity defender, in DamageContext damageContext)
        {
            if (damageContext.HitData.IsUnavoidable)
                return 1;

            var hitData = damageContext.HitData;
            var effectiveHitRatio = GetAttackerEffectiveHit(attacker);
            var effectiveEvasion = GetDefenderEffectiveEvasion(defender);

            var diff = effectiveHitRatio - effectiveEvasion;

            var clamped = Mathf.Clamp(diff, -_config.MaxEffectiveStatDiff, _config.MaxEffectiveStatDiff);

            var slope = _config.MaxStatHitInfluence / _config.MaxEffectiveStatDiff;
            var statHit = _config.EqualStatsHitChance + clamped * slope;

            statHit = Mathf.Clamp01(statHit);

            var finalHitChance = statHit * Mathf.Clamp01(hitData.HitChance);

            return Mathf.Clamp01(finalHitChance);
        }

        private float GetDamageReductionFinal(in DamageContext damageContext)
        {
            var attacker = damageContext.Attacker;
            var penRatio = attacker.UnitBonusStatsData.PenetrationRatio.Value;

            var defender = damageContext.Defender;

            var constitutionStat = defender.UnitStatsData.Constitution.Value;
            var constitutionInfluence = constitutionStat
                                        * _config.MaxConstitutionInfluence
                                        / StatConstants.MaxStatPoints;

            var damageReductionModifier = 1 - constitutionInfluence * (1 - penRatio);
            return damageReductionModifier;
        }

        private float GetAttackerEffectiveHit(IEntity unit)
        {
            var stats = unit.UnitStatsData;
            var effectiveDex = stats.Dexterity.Value * _config.AttackerDexterityInfluence;
            var effectiveLuck = stats.Luck.Value * _config.AttackerLuckInfluence;
            return effectiveDex + effectiveLuck;
        }

        private float GetDefenderEffectiveEvasion(IEntity unit)
        {
            var stats = unit.UnitStatsData;
            var effectiveDex = stats.Dexterity.Value * _config.DefenderDexterityInfluence;
            var effectiveLuck = stats.Luck.Value * _config.DefenderLuckInfluence;
            return effectiveDex + effectiveLuck;
        }

        private void ApplyCriticalDamageMultiplier(in DamageContext damageContext)
        {
            var damageMultiplier = GetCriticalDamageMultiplier(damageContext);
            var damage = damageContext.HitData.Damage;
            damageContext.HitData.Damage = Mathf.RoundToInt(damage * damageMultiplier);
        }

        private float GetCriticalDamageMultiplier(in DamageContext damageContext)
        {
            return GameplayConstants.CriticalDamageMultiplier +
                   damageContext.Attacker.UnitBonusStatsData.CritDamageBonus.Value;
        }
    }
}