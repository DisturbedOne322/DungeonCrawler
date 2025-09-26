using Animations;

namespace Gameplay.Facades
{
    public interface ICombatant : IEntity, ISkillUser
    {
        EvadeAnimator EvadeAnimator { get; }
    }
}