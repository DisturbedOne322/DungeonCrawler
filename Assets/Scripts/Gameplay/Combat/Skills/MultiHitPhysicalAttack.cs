using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "MultiHitPhysicalAttack", menuName = "Gameplay/Skills/MultiHitPhysicalAttack")]
    public class MultiHitPhysicalAttack : OffensiveSkill
    {
        public override bool CanUse(CombatService combatService) => true;
    }
}