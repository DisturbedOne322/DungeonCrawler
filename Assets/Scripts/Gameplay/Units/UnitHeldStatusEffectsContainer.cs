using System.Collections.Generic;
using System.Linq;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.Debuffs.Core;
using UniRx;

namespace Gameplay.Units
{
    public class UnitHeldStatusEffectsContainer
    {
        private readonly List<DefensiveBuffData> _defensive = new();
        private readonly List<HitBuffData> _offensive = new();
        private readonly List<StatBuffData> _statBuffs = new();
        private readonly List<StatDebuffData> _statDebuffs = new();

        public ReactiveCollection<HeldStatusEffectData> All { get; } = new();

        public IReadOnlyList<DefensiveBuffData> DefensiveBuffs => _defensive;
        public IReadOnlyList<HitBuffData> OffensiveBuffs => _offensive;
        public IReadOnlyList<StatBuffData> StatBuffs => _statBuffs;
        public IReadOnlyList<StatDebuffData> StatDebuffs => _statDebuffs;
        
        public void Add(HeldStatusEffectData data)
        {
            if (data == null || !data.Effect)
                return;
            
            All.Add(data);

            switch (data.Effect)
            {
                case DefensiveBuffData defBuff: _defensive.Add(defBuff); break;
                case HitBuffData offBuff: _offensive.Add(offBuff); break;
                case StatBuffData statBuff: _statBuffs.Add(statBuff); break;
                case StatDebuffData statDebuff: _statDebuffs.Add(statDebuff); break;
            }
        }

        public void RemoveFromSource(BaseGameItem source)
        {
            if (!source)
                return;

            var heldStatusEffect = All.First(x => x.Source == source);
            
            All.Remove(heldStatusEffect);

            switch (heldStatusEffect.Effect)
            {
                case DefensiveBuffData defBuff: _defensive.Remove(defBuff); break;
                case HitBuffData offBuff: _offensive.Remove(offBuff); break;
                case StatBuffData statBuff: _statBuffs.Remove(statBuff); break;
                case StatDebuffData statDebuff: _statDebuffs.Remove(statDebuff); break;
            }
        }

        public void Clear()
        {
            All.Clear();
            _defensive.Clear();
            _offensive.Clear();
            _statBuffs.Clear();
            _statDebuffs.Clear();
        }
    }
}