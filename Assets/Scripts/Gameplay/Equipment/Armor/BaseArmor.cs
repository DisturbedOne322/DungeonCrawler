using Gameplay.Combat;
using Gameplay.Combat.Modifiers;
using Gameplay.Combat.Modifiers.Instances.Defensive;
using UnityEngine;

namespace Gameplay.Equipment.Armor
{
    public abstract class BaseArmor : BaseGameItem
    {
        [SerializeField] private OffensiveModifierSo _offensiveModifier;
        [SerializeField] private DefensiveModifierSo _defensiveModifier;
        
        public OffensiveModifierSo OffensiveModifier => _offensiveModifier;
        public DefensiveModifierSo DefensiveModifier => _defensiveModifier;
    }
}