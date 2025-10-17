using System;

namespace Gameplay.StatusEffects.Core
{
    [Flags]
    public enum StatusEffectReapplyType
    {
        None = 0,
        RefreshDuration = 1 << 0,
        Stack = 1 << 1,
        ExtendDuration = 1 << 2,
        LowerDuration = 1 << 3,
        Independent = 1 << 5
    }
}