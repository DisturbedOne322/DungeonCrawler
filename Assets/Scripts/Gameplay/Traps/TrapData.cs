using System.Collections.Generic;
using Data.Constants;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Debuffs.Core;
using UnityEngine;

namespace Gameplay.Traps
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeon + "Trap Data")]
    public class TrapData : BaseGameItem
    {
        [Range(0, 1f)] public float DamageChance = 0.5f;
        public List<SkillData> Skills = new();
        public List<StatDebuffData> Debuffs = new();
    }
}