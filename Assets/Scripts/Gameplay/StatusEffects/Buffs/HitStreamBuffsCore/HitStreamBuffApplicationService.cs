using System;
using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Facades;

namespace Gameplay.StatusEffects.Buffs.HitStreamBuffsCore
{
    public class HitStreamBuffApplicationService
    {
        private const int DefaultBuffsCapacity = 20;
        
        private readonly SortedDictionary<int, List<HitStreamBuffInstance>> _buffsByPriority;
        private readonly Dictionary<HitStreamBuffPriorityType, int> _priorityCache;

        public HitStreamBuffApplicationService()
        {
            _buffsByPriority = new ();
            _priorityCache = new();

            foreach (HitStreamBuffPriorityType priority in Enum.GetValues(typeof(HitStreamBuffPriorityType)))
            {
                var intValue = Convert.ToInt32(priority);
                _buffsByPriority[intValue] = new (DefaultBuffsCapacity);
                _priorityCache[priority] = intValue;
            }
        }

        public void ApplyHitStreamBuffs(IEntity attacker, HitDataStream hitDataStream)
        {
            var buffs = attacker.UnitActiveStatusEffectsData.ActiveHitStreamBuffs;
            if (buffs.Count == 0)
                return;

            foreach (var list in _buffsByPriority.Values)
                list.Clear();

            foreach (var buff in buffs)
            {
                var priorityEnum = buff.PriorityType;
                var priorityInt = _priorityCache[priorityEnum];

                _buffsByPriority[priorityInt].Add(buff);
            }

            foreach (var kv in _buffsByPriority)
            foreach (var buff in kv.Value)
                for (var i = 0; i < buff.Stacks; i++)
                    ApplyBuff(hitDataStream, buff);
        }
        
        private void ApplyBuff(HitDataStream hitDataStream, HitStreamBuffInstance buff)
        {
            buff.HitStreamBuffData.ModifyHitStream(hitDataStream);
        }
    }
}