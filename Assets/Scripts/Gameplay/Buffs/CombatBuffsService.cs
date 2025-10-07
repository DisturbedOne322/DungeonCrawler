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
            var defensiveBuffs = unit.UnitBuffsData.DefensiveBuffs;

            for (int i = offensiveBuffs.Count - 1; i >= 0; i--)
            {
                var buff = offensiveBuffs[i];
                if(buff.TriggerType != triggerType)
                    continue;
                
                AddCombatBuffTo(unit, buff);
            }
            
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

            bool IsValidExpiration(BuffExpirationType expirationType)
            {
                return !(expirationType is 
                    BuffExpirationType.Permanent or 
                    BuffExpirationType.TurnCount);
            }
            
            for (int i = offensiveBuffs.Count - 1; i >= 0; i--)
            {
                if(!IsValidExpiration(expirationType))
                    continue;
                
                var buff = offensiveBuffs[i];
                if(buff.ExpirationType != expirationType)
                    continue;
                
                offensiveBuffs.RemoveAt(i);
            }
            
            for (int i = defensiveBuffs.Count - 1; i >= 0; i--)
            {
                if(!IsValidExpiration(expirationType))
                    continue;
                
                var buff = defensiveBuffs[i];
                if(buff.ExpirationType != expirationType)
                    continue;
                
                defensiveBuffs.RemoveAt(i);
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

            for (int i = activeOffensiveBuffs.Count - 1; i >= 0; i--)
            {
                var buff = activeOffensiveBuffs[i];

                if(buff.ExpirationType != BuffExpirationType.TurnCount)
                    continue;
                
                if (buff.TurnDurationLeft == 0)
                {
                    activeOffensiveBuffs.RemoveAt(i);
                    continue;
                }

                buff.TurnDurationLeft--;
            }
        }
        
        private void ProcessUnitDefensiveBuffs(IEntity buffTarget)
        {
            var activeDefensiveBuffs = buffTarget.UnitActiveBuffsData.ActiveDefensiveBuffs;

            for (int i = activeDefensiveBuffs.Count - 1; i >= 0; i--)
            {
                var buff = activeDefensiveBuffs[i];

                if(buff.ExpirationType != BuffExpirationType.TurnCount)
                    continue;
                
                if (buff.TurnDurationLeft == 0)
                {
                    activeDefensiveBuffs.RemoveAt(i);
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