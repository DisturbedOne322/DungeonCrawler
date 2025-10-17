using System.Collections.Generic;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using Helpers;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.Core
{
    public abstract class BaseBuffProcessor<TBuffData, TBuffInstance> : IBuffProcessor
        where TBuffData : BaseStatusEffectData where TBuffInstance : BaseStatusEffectInstance
    {
        public void EnableBuffsOnTrigger(IGameUnit unit, StatusEffectTriggerType triggerType)
        {
            var buffs = GetBuffData(unit);
            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                var buff = buffs[i];
                if (buff.TriggerType != triggerType)
                    continue;
                AddBuffTo(unit, buff);
            }
        }

        public void ProcessTurn(IEntity buffTarget)
        {
            var buffs = GetActiveBuffs(buffTarget);

            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                var buff = buffs[i];
                if (buff.EffectExpirationType != StatusEffectExpirationType.TurnCount)
                    continue;

                if (buff.TurnDurationLeft == 0)
                {
                    RemoveBuffInstance(buffTarget, buff);
                    continue;
                }

                buff.TurnDurationLeft--;
            }
        }

        public void RemoveBuffsOnAction(IGameUnit unit, StatusEffectExpirationType effectExpirationType)
        {
            if (!StatusEffectsHelper.IsExpirationTypeActionBased(effectExpirationType))
                return;

            var buffs = GetActiveBuffs(unit);
            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                var buff = buffs[i];
                if (buff.EffectExpirationType != effectExpirationType)
                    continue;

                RemoveBuffInstance(unit, buff);
            }
        }

        public void ClearBuffs(IEntity buffTarget)
        {
            var activeBuffs = GetActiveBuffs(buffTarget);

            for (var i = activeBuffs.Count - 1; i >= 0; i--)
                RemoveBuffInstance(buffTarget, activeBuffs[i]);
        }

        protected abstract ReactiveCollection<TBuffData> GetBuffData(IEntity buffTarget);
        protected abstract List<TBuffInstance> GetActiveBuffs(IEntity buffTarget);
        protected abstract TBuffInstance CreateBuffInstance(TBuffData buffData, IEntity buffTarget);

        protected abstract void RemoveBuffInstance(IEntity buffTarget, TBuffInstance buffInstance);

        public void AddBuffTo(IEntity buffTarget, TBuffData buffData)
        {
            var activeBuffs = GetActiveBuffs(buffTarget);

            bool isIndependent = buffData.StatusEffectReapplyType.HasFlag(StatusEffectReapplyType.Independent);

            if (!isIndependent)
            {
                for (var i = activeBuffs.Count - 1; i >= 0; i--)
                {
                    var activeBuff = activeBuffs[i];
                    if (activeBuff.StatusEffectData == buffData)
                    {
                        StatusEffectsHelper.ReapplyStatusEffect(activeBuff, buffData);
                        return;
                    }
                }
            }

            GetActiveBuffs(buffTarget).Add(CreateBuffInstance(buffData, buffTarget));
        }

        private bool IsValidActionExpiration(StatusEffectExpirationType effectExpirationType)
        {
            return effectExpirationType is
                StatusEffectExpirationType.NextAbsoluteAction or
                StatusEffectExpirationType.NextMagicalAction or
                StatusEffectExpirationType.NextPhysicalAction;
        }
    }
}