using UnityEngine;

namespace Gameplay.Combat.Modifiers.Instances.Defensive
{
    public abstract class DefensiveModifierSo : ScriptableObject, IDefensiveModifier
    {
        [SerializeField] private DefensiveModifierPriorityType _priority;

        public DefensiveModifierPriorityType Priority => _priority;

        public abstract int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);
    }
}