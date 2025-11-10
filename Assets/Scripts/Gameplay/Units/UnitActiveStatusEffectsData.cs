using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Gameplay.StatusEffects.Buffs.HitStreamBuffsCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.Debuffs.Core;
using UniRx;

namespace Gameplay.Units
{
    public class UnitActiveStatusEffectsData
    {
        public readonly List<DefensiveBuffInstance> ActiveDefensiveBuffs = new();
        public readonly List<HitStreamBuffInstance> ActiveHitStreamBuffs = new();
        public readonly List<HitBuffInstance> ActiveOffensiveBuffs = new();
        public readonly List<StatBuffInstance> ActiveStatBuffs = new();
        public readonly List<StatDebuffInstance> ActiveStatDebuffs = new();

        public readonly ReactiveCollection<BaseStatusEffectInstance> AllActiveStatusEffects = new();

        public void AddStatusEffect(BaseStatusEffectInstance effect)
        {
            AllActiveStatusEffects.Add(effect);

            switch (effect)
            {
                case DefensiveBuffInstance defBuff: ActiveDefensiveBuffs.Add(defBuff); break;
                case HitBuffInstance offBuff: ActiveOffensiveBuffs.Add(offBuff); break;
                case StatBuffInstance statBuff: ActiveStatBuffs.Add(statBuff); break;
                case StatDebuffInstance statDebuff: ActiveStatDebuffs.Add(statDebuff); break;
                case HitStreamBuffInstance hitStreamBuff: ActiveHitStreamBuffs.Add(hitStreamBuff); break;
            }
        }

        public void RemoveStatusEffect(BaseStatusEffectInstance effect)
        {
            AllActiveStatusEffects.Remove(effect);

            switch (effect)
            {
                case DefensiveBuffInstance defBuff: ActiveDefensiveBuffs.Remove(defBuff); break;
                case HitBuffInstance offBuff: ActiveOffensiveBuffs.Remove(offBuff); break;
                case StatBuffInstance statBuff: ActiveStatBuffs.Remove(statBuff); break;
                case StatDebuffInstance statDebuff: ActiveStatDebuffs.Remove(statDebuff); break;
                case HitStreamBuffInstance hitStreamBuff: ActiveHitStreamBuffs.Remove(hitStreamBuff); break;
            }
        }

        public bool IsStatusEffectActive(BaseStatusEffectData data)
        {
            switch (data)
            {
                case DefensiveBuffData:
                    return IsStatusEffectActive(ActiveDefensiveBuffs, data);
                case HitBuffData:
                    return IsStatusEffectActive(ActiveOffensiveBuffs, data);
                case StatBuffData:
                    return IsStatusEffectActive(ActiveStatBuffs, data);
                case StatDebuffData:
                    return IsStatusEffectActive(ActiveStatDebuffs, data);
                case HitStreamBuffData:
                    return IsStatusEffectActive(ActiveHitStreamBuffs, data);
            }

            return false;
        }

        private bool IsStatusEffectActive<T>(List<T> activeStatusEffects,
            BaseStatusEffectData searchedStatusEffect) where T : BaseStatusEffectInstance
        {
            foreach (var statusEffect in activeStatusEffects)
                if (statusEffect.StatusEffectData == searchedStatusEffect)
                    return true;

            return false;
        }
    }
}