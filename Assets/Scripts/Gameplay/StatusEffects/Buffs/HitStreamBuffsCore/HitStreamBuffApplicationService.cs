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

        public void ApplyStructuralHitStreamBuffs(IEntity attacker, HitDataStream hitDataStream) => ApplyHitStreamBuffsFiltered(attacker, hitDataStream, p => p == HitStreamBuffPriorityType.StructuralChange);

        public void ApplyHitStreamBuffs(IEntity attacker, HitDataStream hitDataStream) => ApplyHitStreamBuffsFiltered(attacker, hitDataStream, p => p != HitStreamBuffPriorityType.StructuralChange);

        private void ApplyHitStreamBuffsFiltered(
            IEntity attacker, 
            HitDataStream hitDataStream, 
            Func<HitStreamBuffPriorityType, bool> filter)
        {
            var buffs = attacker.UnitActiveStatusEffectsData.ActiveHitStreamBuffs;
            if (buffs.Count == 0)
                return;

            foreach (var list in _buffsByPriority.Values)
                list.Clear();

            foreach (var buff in buffs)
            {
                if (!filter(buff.PriorityType))
                    continue;

                _buffsByPriority[(int)buff.PriorityType].Add(buff);
            }

            foreach (var kv in _buffsByPriority)
            foreach (var buff in kv.Value)
            {
                int stacks = buff.Stacks.Value;
                for (var i = 0; i < stacks; i++)
                    ApplyBuff(hitDataStream, buff);
            }
        }

        
        private void ApplyBuff(HitDataStream hitDataStream, HitStreamBuffInstance buff) => buff.HitStreamBuffData.ModifyHitStream(hitDataStream);
    }
}