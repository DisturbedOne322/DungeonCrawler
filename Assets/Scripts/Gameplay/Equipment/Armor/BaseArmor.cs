using Gameplay.Buffs;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
using Gameplay.Buffs.StatBuffsCore;
using Gameplay.Combat;
using UnityEngine;

namespace Gameplay.Equipment.Armor
{
    public abstract class BaseArmor : BaseGameItem
    {
        [SerializeField] private OffensiveBuffData _offensiveBuff;
        [SerializeField] private DefensiveBuffData _defensiveBuff;
        [SerializeField] private StatBuffData _statBuff;
        
        
        public OffensiveBuffData OffensiveBuff => _offensiveBuff;
        public DefensiveBuffData DefensiveBuff => _defensiveBuff;
        public StatBuffData StatBuff => _statBuff;
    }
}