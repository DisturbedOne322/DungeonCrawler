using Animations;
using Gameplay.Visual;

namespace Gameplay.Facades
{
    public interface ICombatant : IEntity, ISkillUser
    {
        EvadeAnimator EvadeAnimator { get; }
        AttackAnimator AttackAnimator { get; }
    }
}