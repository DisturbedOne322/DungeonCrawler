using System.Collections.Generic;
using Gameplay.Buffs.Core;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
using Gameplay.Buffs.StatBuffsCore;
using Gameplay.Facades;

namespace Gameplay.Buffs.Services
{
    public class CombatBuffsService
    {
        private readonly OffensiveBuffProcessor _offensiveBuffProcessor;
        private readonly DefensiveBuffProcessor _defensiveBuffProcessor;
        private readonly StatBuffProcessor _statBuffProcessor;
        private readonly IBuffProcessor[] _buffProcessors;

        public CombatBuffsService()
        {
            _offensiveBuffProcessor = new ();
            _defensiveBuffProcessor = new ();
            _statBuffProcessor = new ();
            
            _buffProcessors = new IBuffProcessor[]
            {
                _offensiveBuffProcessor,
                _defensiveBuffProcessor,
                _statBuffProcessor
            };
        }
        

        public void AddOffensiveBuffTo(IGameUnit buffTarget, OffensiveBuffData buffData) => _offensiveBuffProcessor.AddBuffTo(buffTarget, buffData);
        public void AddDefensiveBuffTo(IGameUnit buffTarget, DefensiveBuffData buffData) => _defensiveBuffProcessor.AddBuffTo(buffTarget, buffData);
        public void AddStatBuffTo(IGameUnit buffTarget, StatBuffData buffData) => _statBuffProcessor.AddBuffTo(buffTarget, buffData);

        public void ApplyPermanentBuffs(IEntity buffTarget)
        {
            foreach (var processor in _buffProcessors)
            {
                processor.ApplyPermanentBuffs(buffTarget);
            }
        }

        public void ClearCombatBuffs(IEntity buffTarget)
        {
            foreach (var processor in _buffProcessors)
            {
                processor.ClearBuffs(buffTarget);
            }
        }

        public void ProcessTurn(IEntity buffTarget)
        {
            foreach (var processor in _buffProcessors)
            {
                processor.ProcessTurn(buffTarget);
            }
        }

        public void EnableBuffsOnTrigger(IGameUnit unit, BuffTriggerType triggerType)
        {
            foreach (var processor in _buffProcessors)
            {
                processor.EnableBuffsOnTrigger(unit, triggerType);
            }
        }

        public void RemoveBuffsOnAction(IGameUnit unit, BuffExpirationType expirationType)
        {
            foreach (var processor in _buffProcessors)
            {
                processor.RemoveBuffsOnAction(unit, expirationType);
            }
        }
    }
}