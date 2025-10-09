using System.Collections.Generic;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
using Gameplay.Buffs.StatBuffsCore;

namespace Gameplay.Combat.Data
{
    public class UnitActiveBuffsData
    {
        public readonly List<OffensiveBuffInstance> ActiveOffensiveBuffs = new ();
        public readonly List<DefensiveBuffInstance> ActiveDefensiveBuffs = new ();
        public readonly List<StatBuffInstance> ActiveStatBuffs = new ();
        
        public bool IsOffensiveBuffActive(OffensiveBuffData buff)
        {
            for(int i = 0; i < ActiveOffensiveBuffs.Count; i++)
                if(ActiveOffensiveBuffs[i].BuffData == buff)
                    return true;
            
            return false;
        }
        
        public bool IsDefensiveBuffActive(DefensiveBuffData buff)
        {
            for(int i = 0; i < ActiveOffensiveBuffs.Count; i++)
                if(ActiveDefensiveBuffs[i].BuffData == buff)
                    return true;
            
            return false;
        }
        
        public bool IsStatBuffActive(StatBuffData buffData)
        {
            for(int i = 0; i < ActiveStatBuffs.Count; i++)
                if(ActiveStatBuffs[i].BuffData == buffData)
                    return true;
            
            return false;
        }
    }
}