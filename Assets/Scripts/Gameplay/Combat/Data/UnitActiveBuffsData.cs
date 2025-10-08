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
            for (int i = 0; i < ActiveOffensiveBuffs.Count; i++)
            {
                var activeBuff = ActiveOffensiveBuffs[i];
                
                if (activeBuff.BuffData == buff)
                {
                    activeBuff.TurnDurationLeft = activeBuff.BuffData.BuffDurationData.TurnDurations;
                    ActiveOffensiveBuffs[i] = activeBuff;
                    return true;
                }
            }
            
            return false;
        }
        
        public bool RefreshIfActive(DefensiveBuffData buff)
        {
            for (int i = 0; i < ActiveDefensiveBuffs.Count; i++)
            {
                var activeBuff = ActiveDefensiveBuffs[i];
                if (activeBuff.BuffData == buff)
                {
                    activeBuff.TurnDurationLeft = activeBuff.BuffData.BuffDurationData.TurnDurations;
                    ActiveDefensiveBuffs[i] = activeBuff;
                    return true;
                }
            }

            
            return false;
        }
        
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
    }
}