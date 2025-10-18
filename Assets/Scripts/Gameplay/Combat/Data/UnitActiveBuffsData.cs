using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.OffensiveCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;

namespace Gameplay.Combat.Data
{
    public class UnitActiveBuffsData
    {
        public readonly List<DefensiveBuffInstance> ActiveDefensiveBuffs = new();
        public readonly List<OffensiveBuffInstance> ActiveOffensiveBuffs = new();
        public readonly List<StatBuffInstance> ActiveStatBuffs = new();

        public bool IsOffensiveBuffActive(OffensiveBuffData buff)
        {
            for (var i = 0; i < ActiveOffensiveBuffs.Count; i++)
                if (ActiveOffensiveBuffs[i].StatusEffectData == buff)
                    return true;

            return false;
        }

        public bool IsDefensiveBuffActive(DefensiveBuffData buff)
        {
            for (var i = 0; i < ActiveDefensiveBuffs.Count; i++)
                if (ActiveDefensiveBuffs[i].StatusEffectData == buff)
                    return true;

            return false;
        }

        public bool IsStatBuffActive(StatBuffData buffData)
        {
            for (var i = 0; i < ActiveStatBuffs.Count; i++)
                if (ActiveStatBuffs[i].StatusEffectData == buffData)
                    return true;

            return false;
        }
    }
}