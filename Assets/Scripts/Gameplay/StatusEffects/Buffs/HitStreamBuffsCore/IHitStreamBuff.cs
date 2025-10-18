using Gameplay.Combat.Data;

namespace Gameplay.StatusEffects.Buffs.HitStreamBuffsCore
{
    public interface IHitStreamBuff
    {
        HitStreamBuffPriorityType Priority { get; }
        
        void ModifyHitStream(HitDataStream hitDataStream);
    }
}