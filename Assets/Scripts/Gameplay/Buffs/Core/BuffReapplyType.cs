namespace Gameplay.Buffs.Core
{
    [System.Flags]
    public enum BuffReapplyType
    {
        RefreshDuration,
        Stack,
        Ignore,
        ExtendDuration,
        LowerDuration
    }
}