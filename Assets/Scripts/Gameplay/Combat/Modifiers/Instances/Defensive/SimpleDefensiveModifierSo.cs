using UnityEngine;

namespace Gameplay.Combat.Modifiers.Instances.Defensive
{
    [CreateAssetMenu(fileName = "SimpleDefensiveModifierSo", menuName = "Modifiers/Defensive/SimpleDefensiveModifierSo")]
    public class SimpleDefensiveModifierSo : DefensiveModifierSo
    {
        [SerializeField, Min(0)] private int _reducedDamage;
        
        public override int ModifyIngoingDamage(int currentDamage, in DamageContext ctx)
        {
            return currentDamage - _reducedDamage;
        }
    }
}