using System.Collections.Generic;
using Gameplay.Buffs;

namespace Gameplay.Combat.Data
{
    public class UnitActiveBuffsData
    {
        public readonly List<OffensiveBuffInstance> ActiveOffensiveBuffs = new ();
        public readonly List<DefensiveBuffInstance> ActiveDefensiveBuffs = new ();

        public bool RefreshIfActive(OffensiveBuffData buff)
        {
            for(int i = 0; i < ActiveOffensiveBuffs.Count; i++)
                if (ActiveOffensiveBuffs[i].OffensiveBuffData == buff)
                {
                    var buffData = ActiveOffensiveBuffs[i];
                    buffData.TurnDurationLeft = ActiveOffensiveBuffs[i].OffensiveBuffData.BuffDurationData.TurnDurations;
                    ActiveOffensiveBuffs[i] = buffData;
                    return true;
                }
            
            return false;
        }
        
        public bool RefreshIfActive(DefensiveBuffData buff)
        {
            for(int i = 0; i < ActiveOffensiveBuffs.Count; i++)
                if (ActiveDefensiveBuffs[i].DefensiveBuffData == buff)
                {
                    var buffData = ActiveDefensiveBuffs[i];
                    buffData.TurnDurationLeft = ActiveDefensiveBuffs[i].DefensiveBuffData.BuffDurationData.TurnDurations;
                    ActiveDefensiveBuffs[i] = buffData;
                    return true;
                }
            
            return false;
        }
        
        public bool IsOffensiveBuffActive(OffensiveBuffData buff)
        {
            for(int i = 0; i < ActiveOffensiveBuffs.Count; i++)
                if(ActiveOffensiveBuffs[i].OffensiveBuffData == buff)
                    return true;
            
            return false;
        }
        
        public bool IsDefensiveBuffActive(DefensiveBuffData buff)
        {
            for(int i = 0; i < ActiveOffensiveBuffs.Count; i++)
                if(ActiveDefensiveBuffs[i].DefensiveBuffData == buff)
                    return true;
            
            return false;
        }
    }
}