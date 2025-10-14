namespace Gameplay.Buffs.Core
{
    [System.Flags]
    public enum BuffReapplyType
    {
        None            = 0,
        RefreshDuration = 1 << 0,
        Stack           = 1 << 1,
        ExtendDuration  = 1 << 2,
        LowerDuration   = 1 << 3
    }
}