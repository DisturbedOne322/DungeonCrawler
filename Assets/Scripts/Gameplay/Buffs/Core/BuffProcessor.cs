using System.Collections.Generic;
using Gameplay.Facades;
using Helpers;
using UniRx;
using UnityEngine;

namespace Gameplay.Buffs.Core
{
    public abstract class BaseBuffProcessor<TBuffData, TBuffInstance> : IBuffProcessor
        where TBuffData : BaseBuffData where TBuffInstance : BaseBuffInstance
    {
        protected abstract ReactiveCollection<TBuffData> GetBuffData(IEntity buffTarget);
        protected abstract List<TBuffInstance> GetActiveBuffs(IEntity buffTarget);
        protected abstract TBuffInstance CreateBuffInstance(TBuffData buffData, IEntity buffTarget);
        
        protected abstract void RemoveBuffInstance(IEntity buffTarget, TBuffInstance buffInstance);

        public void AddBuffTo(IEntity buffTarget, TBuffData buffData)
        {
            var activeBuffs = GetActiveBuffs(buffTarget);
            
            for (int i = activeBuffs.Count - 1; i >= 0; i--)
            {
                var activeBuff = activeBuffs[i];
                if (activeBuff.BuffData == buffData)
                {
                    BuffsHelper.ReapplyBuff(activeBuff, buffData);
                    return;
                }
            }
            
            GetActiveBuffs(buffTarget).Add(CreateBuffInstance(buffData, buffTarget));
        }

        public void EnableBuffsOnTrigger(IGameUnit unit, BuffTriggerType triggerType)
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
                if (buff.ExpirationType != BuffExpirationType.TurnCount)
                    continue;
                
                if (buff.TurnDurationLeft == 0)
                {
                    RemoveBuffInstance(buffTarget, buff);
                    continue;
                }

                buff.TurnDurationLeft--;
            }
        }

        public void RemoveBuffsOnAction(IGameUnit unit, BuffExpirationType expirationType)
        {
            if (!IsValidActionExpiration(expirationType))
                return;

            var buffs = GetActiveBuffs(unit);
            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                var buff = buffs[i];
                if (buff.ExpirationType != expirationType)
                    continue;
                
                RemoveBuffInstance(unit, buff);
            }
        }

        public void ClearBuffs(IEntity buffTarget)
        {
            var activeBuffs = GetActiveBuffs(buffTarget);

            for (int i = activeBuffs.Count - 1; i >= 0; i--) 
                RemoveBuffInstance(buffTarget, activeBuffs[i]);
        }

        private bool IsValidActionExpiration(BuffExpirationType expirationType)
        {
            return expirationType is 
                BuffExpirationType.NextAbsoluteAction or 
                BuffExpirationType.NextMagicalAction or 
                BuffExpirationType.NextPhysicalAction;
        }
    }
}