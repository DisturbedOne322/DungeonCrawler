using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Debuffs
{
    public class CombatDebuffsService
    {
        private readonly StatDebuffsProcessor _statDebuffsProcessor;

        public CombatDebuffsService(UnitStatsModificationService unitStatsModificationService)
        {
            _statDebuffsProcessor = new(unitStatsModificationService);
        }
        
        public void AddStatDebuffTo(IGameUnit buffTarget, StatDebuffData debuffData)
        {
            _statDebuffsProcessor.AddDebuffTo(buffTarget, debuffData);
        }

        public void ClearCombatDebuffs(IEntity debuffTarget)
        {
            _statDebuffsProcessor.ClearDebuffs(debuffTarget);
        }

        public void ProcessTurn(IEntity debuffTarget)
        {
            _statDebuffsProcessor.ProcessTurn(debuffTarget);
        }

        public void EnableDebuffsOnTrigger(IGameUnit debuffHolder, IGameUnit debuffTarget, StatusEffectTriggerType triggerType) => _statDebuffsProcessor.EnableDebuffsOnTrigger(debuffHolder, debuffTarget, triggerType);
    }
}