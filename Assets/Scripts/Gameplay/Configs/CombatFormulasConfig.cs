using Data.Constants;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayConfigs + "CombatFormulasConfig")]
    public class CombatFormulasConfig : GameplayConfig
    {
        [Header("Hit Chance")] [Min(0)] public float AttackerDexterityInfluence = 1;

        [Min(0)] public float AttackerLuckInfluence = 0.5f;

        [Min(0)] public float DefenderDexterityInfluence = 1f;
        [Min(0)] public float DefenderLuckInfluence = 0.5f;

        [Range(0, 1f)] public float EqualStatsHitChance;
        [Min(0)] public int MaxEffectiveStatDiff = 50;
        [Range(0, 1f)] public float MaxStatHitInfluence = 0.25f;

        [Header("Crit Chance")] [Space] [Range(0, 1f)]
        public float MaxDexterityCritInfluence = 0.25f;

        [Range(0, 1f)] public float MaxLuckCritInfluence = 0.35f;

        [Header("Damage Reduction")] [Space] [Range(0, 1f)]
        public float MaxConstitutionInfluence = 0.8f;

        [Min(0.1f)] public float MinDefenseMultiplier = 0.5f;
    }
}