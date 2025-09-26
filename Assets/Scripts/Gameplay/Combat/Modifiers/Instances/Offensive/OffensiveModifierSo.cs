using UnityEngine;

namespace Gameplay.Combat.Modifiers
{
    public abstract class OffensiveModifierSo : ScriptableObject, IOffensiveModifier
    {
        [SerializeField] private OffensiveModifierPriorityType _priority;
        
        public OffensiveModifierPriorityType Priority => _priority;

        public abstract int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}