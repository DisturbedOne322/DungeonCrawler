using UnityEngine;

namespace Gameplay.Combat.Modifiers
{
    [CreateAssetMenu(fileName = "SimpleOffensiveModifierSo", menuName = "Modifiers/Offensive/SimpleOffensiveModifier")]
    public class SimpleOffensiveModifierSo : OffensiveModifierSo
    {
        [SerializeField, Min(0)] private int _damageIncrease;
        
        public override int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx)
        {
            return currentDamage + _damageIncrease;
        }
    }
}