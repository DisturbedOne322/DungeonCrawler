using System.Collections.Generic;
using System.Linq;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Buffs
{
    public class CombatBuffsService
    {
        public void ApplyPermanentBuffs(IEntity buffTarget)
        {
            var offensiveBuffs = buffTarget.UnitBuffsData.OffensiveBuffs;
            var defensiveBuffs = buffTarget.UnitBuffsData.DefensiveBuffs;

            var permaOffensiveBuffs =
                offensiveBuffs.Where(buff => buff.BuffDurationData.ExpirationType == BuffExpirationType.Permanent);
            
            var permaDefensiveBuffs =
                defensiveBuffs.Where(buff => buff.BuffDurationData.ExpirationType == BuffExpirationType.Permanent);

            foreach (var buff in permaOffensiveBuffs) 
                AddCombatBuffTo(buffTarget, buff);
            
            foreach (var buff in permaDefensiveBuffs) 
                AddCombatBuffTo(buffTarget, buff);
        }

        public void ClearCombatBuffs(IEntity buffTarget)
        {
            buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs.Clear();
            buffTarget.UnitActiveBuffsData.ActiveDefensiveBuffs.Clear();
        }

        public void ProcessTurn(IEntity buffTarget)
        {
            ProcessUnitOffensiveBuffs(buffTarget);
            ProcessUnitDefensiveBuffs(buffTarget);
        }

        public void EnableBuffsOnTrigger(IGameUnit unit, BuffTriggerType triggerType)
        {
            var offensiveBuffs = unit.UnitBuffsData.OffensiveBuffs;

            for (int i = offensiveBuffs.Count - 1; i >= 0; i--)
            {
                var buff = offensiveBuffs[i];
                if(buff.TriggerType != triggerType)
                    continue;
                
                Debug.Log(buff.TriggerType);
                
                AddCombatBuffTo(unit, buff);
            }
            
            var defensiveBuffs = unit.UnitBuffsData.DefensiveBuffs;

            for (int i = defensiveBuffs.Count - 1; i >= 0; i--)
            {
                var buff = defensiveBuffs[i];
                if(buff.TriggerType != triggerType)
                    continue;
                
                AddCombatBuffTo(unit, buff);
            }
        }

        public void RemoveBuffsOnAction(IGameUnit unit, BuffExpirationType expirationType)
        {
            var offensiveBuffs = unit.UnitActiveBuffsData.ActiveOffensiveBuffs;
            var defensiveBuffs = unit.UnitActiveBuffsData.ActiveDefensiveBuffs;

            RemoveBuffsOnAction(offensiveBuffs, expirationType);
            RemoveBuffsOnAction(defensiveBuffs, expirationType);
        }

        private void RemoveBuffsOnAction<T>(List<T> buffs, BuffExpirationType expirationType) where T : BaseBuffInstance
        {
            bool IsValidExpiration(BuffExpirationType expirationType)
            {
                return !(expirationType is 
                    BuffExpirationType.Permanent or 
                    BuffExpirationType.TurnCount);
            }
            
            for (int i = buffs.Count - 1; i >= 0; i--)
            {
                if(!IsValidExpiration(expirationType))
                    continue;
                
                var buff = buffs[i];
                if(buff.ExpirationType != expirationType)
                    continue;
                
                buffs.RemoveAt(i);
            }
        }

        public void AddCombatBuffTo(IEntity buffTarget, OffensiveBuffData buffData)
        {
            if (buffTarget.UnitActiveBuffsData.RefreshIfActive(buffData))
                return;
            
            buffTarget.UnitActiveBuffsData.
                ActiveOffensiveBuffs.Add(CreateOffensiveBuffInstance(buffData));
        }

        public void AddCombatBuffTo(IEntity buffTarget, DefensiveBuffData buffData)
        {
            if (buffTarget.UnitActiveBuffsData.RefreshIfActive(buffData))
                return;
            
            buffTarget.UnitActiveBuffsData.
                ActiveDefensiveBuffs.Add(CreateDefensiveBuffInstance(buffData));
        }
        
        private void ProcessUnitOffensiveBuffs(IEntity buffTarget)
        {
            var activeOffensiveBuffs = buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs;
            ProcessUnitTurnBuffs(activeOffensiveBuffs);
        }
        
        private void ProcessUnitDefensiveBuffs(IEntity buffTarget)
        {
            var activeDefensiveBuffs = buffTarget.UnitActiveBuffsData.ActiveDefensiveBuffs;
            ProcessUnitTurnBuffs(activeDefensiveBuffs);
        }

        private void ProcessUnitTurnBuffs<T>(List<T> buffs)
            where T : BaseBuffInstance
        {
            for (int i = buffs.Count - 1; i >= 0; i--)
            {
                var buff = buffs[i];

                if (buff.ExpirationType != BuffExpirationType.TurnCount)
                    continue;

                if (buff.TurnDurationLeft == 0)
                {
                    buffs.RemoveAt(i);
                    continue;
                }

                buff.TurnDurationLeft--;
            }
        }

        private OffensiveBuffInstance CreateOffensiveBuffInstance(OffensiveBuffData buffData) => 
            OffensiveBuffInstance.Create(buffData);

        private DefensiveBuffInstance CreateDefensiveBuffInstance(DefensiveBuffData buffData) => 
            DefensiveBuffInstance.Create(buffData);
    }
}