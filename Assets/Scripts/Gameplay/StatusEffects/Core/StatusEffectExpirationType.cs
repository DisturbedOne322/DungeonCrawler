namespace Gameplay.StatusEffects.Core
{
    public enum StatusEffectExpirationType
    {
        Permanent,
        NextPhysicalAction,
        NextMagicalAction,
        NextAbsoluteAction,
        TurnCount,
        CombatEnd,
        DepthIncrease,
        Reapply
    }
}