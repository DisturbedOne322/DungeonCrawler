using Gameplay.Buffs;
using Gameplay.Combat;
using UnityEngine;

namespace Gameplay.Equipment.Armor
{
    public abstract class BaseArmor : BaseGameItem
    {
        [SerializeField] private OffensiveBuffData _offensiveBuff;
        [SerializeField] private DefensiveBuffData _defensiveBuff;
        
        public OffensiveBuffData OffensiveBuff => _offensiveBuff;
        public DefensiveBuffData DefensiveBuff => _defensiveBuff;
    }
}