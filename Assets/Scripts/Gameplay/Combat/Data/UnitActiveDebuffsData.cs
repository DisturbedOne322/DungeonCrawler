using System.Collections.Generic;
using Gameplay.StatusEffects.Debuffs;

namespace Gameplay.Combat.Data
{
    public class UnitActiveDebuffsData
    {
        public readonly List<StatDebuffInstance> ActiveStatDebuffs = new();

        public bool IsStatDebuffActive(StatDebuffData debuffData)
        {
            for (var i = 0; i < ActiveStatDebuffs.Count; i++)
                if (ActiveStatDebuffs[i].StatusEffectData == debuffData)
                    return true;

            return false;
        }
    }
}