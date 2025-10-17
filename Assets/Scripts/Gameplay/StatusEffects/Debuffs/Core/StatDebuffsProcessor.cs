using System.Collections.Generic;
using Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Helpers;
using UniRx;
using UnityEngine;

namespace Gameplay.StatusEffects.Debuffs
{
    public class StatDebuffsProcessor
    {
        private readonly UnitStatsModificationService _unitStatsModificationService;

        public StatDebuffsProcessor(UnitStatsModificationService unitStatsModificationService)
        {
            _unitStatsModificationService = unitStatsModificationService;
        }
        
        public void EnableDebuffsOnTrigger(IGameUnit debuffHolder, IGameUnit debuffTarget, StatusEffectTriggerType triggerType)
        {
            var debuffs = GetUnitDebuffs(debuffHolder);
            for (var i = debuffs.Count - 1; i >= 0; i--)
            {
                var debuff = debuffs[i];
                if (debuff.TriggerType != triggerType)
                    continue;
                
                AddDebuffTo(debuffTarget, debuff);
            }
        }

        public void ProcessTurn(IEntity buffTarget)
        {
            var buffs = GetActiveDebuffs(buffTarget);

            for (var i = buffs.Count - 1; i >= 0; i--)
            {
                var buff = buffs[i];
                if (buff.EffectExpirationType != StatusEffectExpirationType.TurnCount)
                    continue;

                if (buff.TurnDurationLeft == 0)
                {
                    RemoveDebuffInstance(buffTarget, buff);
                    continue;
                }

                buff.TurnDurationLeft--;
            }
        }

        public void ClearDebuffs(IEntity buffTarget)
        {
            var activeDebuffs = GetActiveDebuffs(buffTarget);

            for (var i = activeDebuffs.Count - 1; i >= 0; i--)
                RemoveDebuffInstance(buffTarget, activeDebuffs[i]);
        }

        public void AddDebuffTo(IEntity debuffTarget, StatDebuffData debuffData)
        {
            var activeDebuffs = GetActiveDebuffs(debuffTarget);

            bool isIndependent = debuffData.StatusEffectReapplyType.HasFlag(StatusEffectReapplyType.Independent);

            if (!isIndependent)
                for (var i = activeDebuffs.Count - 1; i >= 0; i--)
                {
                    var activeBuff = activeDebuffs[i];
                    if (activeBuff.StatusEffectData == debuffData)
                    {
                        StatusEffectsHelper.ReapplyStatusEffect(activeBuff, debuffData);
                        return;
                    }
                }

            GetActiveDebuffs(debuffTarget).Add(CreateDebuffInstance(debuffData, debuffTarget));
        }

        private ReactiveCollection<StatDebuffData> GetUnitDebuffs(IEntity holderUnit) => holderUnit.UnitDebuffsData.StatDebuffs;
        private List<StatDebuffInstance> GetActiveDebuffs(IEntity debuffTarget) => debuffTarget.UnitActiveDebuffsData.ActiveStatDebuffs;

        private StatDebuffInstance CreateDebuffInstance(StatDebuffData debuffData, IEntity debuffTarget)
        {
            var instance = debuffData.CreateDebuffInstance(debuffTarget);
            instance.ValueChange = _unitStatsModificationService.ModifyStat(debuffTarget, instance.StatType, -instance.ValueChange);
            return instance;
        }

        private void RemoveDebuffInstance(IEntity debuffTarget, StatDebuffInstance debuffInstance)
        {
            debuffTarget.UnitActiveDebuffsData.ActiveStatDebuffs.Remove(debuffInstance);
            _unitStatsModificationService.ModifyStat(debuffTarget, debuffInstance.StatType, -debuffInstance.ValueChange);
        }
    }
}