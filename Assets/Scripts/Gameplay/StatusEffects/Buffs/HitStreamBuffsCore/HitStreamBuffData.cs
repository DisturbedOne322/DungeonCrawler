using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.HitStreamBuffsCore
{
    public abstract class HitStreamBuffData : BaseStatusEffectData, IHitStreamBuff
    {
        [SerializeField] private HitStreamBuffPriorityType _priority;

        public HitStreamBuffPriorityType Priority => _priority;

        public override BaseStatusEffectInstance CreateInstance() => HitStreamBuffInstance.Create(this);

        public abstract void ModifyHitStream(HitDataStream hitDataStream);
    }
}