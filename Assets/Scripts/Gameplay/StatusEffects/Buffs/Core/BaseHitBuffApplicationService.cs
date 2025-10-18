using System;
using System.Collections.Generic;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.Core
{
    public abstract class BaseHitBuffApplicationService<TBuffInstance, TPriorityType>
        where TBuffInstance : BaseStatusEffectInstance
        where TPriorityType : Enum
    {
        private const int DefaultBuffsCapacity = 20;

        private readonly SortedDictionary<int, List<TBuffInstance>> _buffsByPriority;
        private readonly Dictionary<TPriorityType, int> _priorityCache;

        protected BaseHitBuffApplicationService()
        {
            _buffsByPriority = new SortedDictionary<int, List<TBuffInstance>>();
            _priorityCache = new Dictionary<TPriorityType, int>();

            foreach (TPriorityType priority in Enum.GetValues(typeof(TPriorityType)))
            {
                var intValue = Convert.ToInt32(priority);
                _buffsByPriority[intValue] = new List<TBuffInstance>(DefaultBuffsCapacity);
                _priorityCache[priority] = intValue;
            }
        }

        protected abstract List<TBuffInstance> GetActiveBuffs(in DamageContext damageContext);

        protected abstract bool CanApplyBuffs(in DamageContext damageContext);

        protected abstract int ApplyBuff(TBuffInstance buff, int damage, in DamageContext damageContext);

        protected abstract TPriorityType GetPriorityType(TBuffInstance buff);

        public int CalculateDamage(in DamageContext damageContext)
        {
            var result = damageContext.HitData.Damage;

            var buffs = GetActiveBuffs(damageContext);
            if (buffs.Count == 0 || !CanApplyBuffs(damageContext))
                return result;

            foreach (var list in _buffsByPriority.Values)
                list.Clear();

            foreach (var buff in buffs)
            {
                var priorityEnum = GetPriorityType(buff);
                var priorityInt = _priorityCache[priorityEnum];

                _buffsByPriority[priorityInt].Add(buff);
            }

            foreach (var kv in _buffsByPriority)
            foreach (var buff in kv.Value)
                for (var i = 0; i < buff.Stacks; i++)
                    result = ApplyBuff(buff, result, damageContext);

            return result;
        }
    }
}