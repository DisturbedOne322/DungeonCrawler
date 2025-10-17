using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Core;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.OffensiveCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class CombatBuffsService
    {
        private readonly IBuffProcessor[] _buffProcessors;
        private readonly DefensiveBuffProcessor _defensiveBuffProcessor;
        private readonly OffensiveBuffProcessor _offensiveBuffProcessor;
        private readonly StatBuffProcessor _statBuffProcessor;

        public CombatBuffsService(UnitStatsModificationService unitStatsModificationService)
        {
            _offensiveBuffProcessor = new OffensiveBuffProcessor();
            _defensiveBuffProcessor = new DefensiveBuffProcessor();
            _statBuffProcessor = new StatBuffProcessor(unitStatsModificationService);

            _buffProcessors = new IBuffProcessor[]
            {
                _offensiveBuffProcessor,
                _defensiveBuffProcessor,
                _statBuffProcessor,
            };
        }

        public void AddOffensiveBuffTo(IGameUnit buffTarget, OffensiveStatusEffectData statusEffectData)
        {
            _offensiveBuffProcessor.AddBuffTo(buffTarget, statusEffectData);
        }

        public void AddDefensiveBuffTo(IGameUnit buffTarget, DefensiveBuffData buffData)
        {
            _defensiveBuffProcessor.AddBuffTo(buffTarget, buffData);
        }

        public void AddStatBuffTo(IGameUnit buffTarget, StatBuffData buffData)
        {
            _statBuffProcessor.AddBuffTo(buffTarget, buffData);
        }

        public void ClearCombatBuffs(IEntity buffTarget)
        {
            foreach (var processor in _buffProcessors)
                processor.ClearBuffs(buffTarget);
        }

        public void ProcessTurn(IEntity buffTarget)
        {
            foreach (var processor in _buffProcessors)
                processor.ProcessTurn(buffTarget);
        }

        public void EnableBuffsOnTrigger(IGameUnit unit, StatusEffectTriggerType triggerType)
        {
            foreach (var processor in _buffProcessors)
                processor.EnableBuffsOnTrigger(unit, triggerType);
        }

        public void RemoveBuffsOnAction(IGameUnit unit, StatusEffectExpirationType effectExpirationType)
        {
            foreach (var processor in _buffProcessors)
                processor.RemoveBuffsOnAction(unit, effectExpirationType);
        }
    }
}