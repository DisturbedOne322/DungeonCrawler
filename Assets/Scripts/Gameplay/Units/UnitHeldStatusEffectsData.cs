using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.Debuffs.Core;
using UniRx;

namespace Gameplay.Units
{
    public class UnitHeldStatusEffectsData
    {
        private readonly UnitActiveStatusEffectsData _activeStatusEffects;
        
        private readonly ReactiveCollection<BaseStatusEffectData> _all = new();
        private readonly List<DefensiveBuffData> _defensive = new();
        private readonly List<HitBuffData> _offensive = new();
        private readonly List<StatBuffData> _statBuffs = new();
        private readonly List<StatDebuffData> _statDebuffs = new();

        public ReactiveCollection<BaseStatusEffectData> All => _all;
        public IReadOnlyList<DefensiveBuffData> DefensiveBuffs => _defensive;
        public IReadOnlyList<HitBuffData> OffensiveBuffs => _offensive;
        public IReadOnlyList<StatBuffData> StatBuffs => _statBuffs;
        public IReadOnlyList<StatDebuffData> StatDebuffs => _statDebuffs;

        public UnitHeldStatusEffectsData(UnitActiveStatusEffectsData activeStatusEffects)
        {
            _activeStatusEffects = activeStatusEffects;
        }
        
        public void Add(BaseStatusEffectData data)
        {
            if (!data)
                return;

            _all.Add(data);

            switch (data)
            {
                case DefensiveBuffData defBuff: _defensive.Add(defBuff); break;
                case HitBuffData offBuff: _offensive.Add(offBuff); break;
                case StatBuffData statBuff: _statBuffs.Add(statBuff); break;
                case StatDebuffData statDebuff: _statDebuffs.Add(statDebuff); break;
            }
        }

        public void Remove(BaseStatusEffectData data)
        {
            if (!data)
                return;

            _all.Remove(data);

            switch (data)
            {
                case DefensiveBuffData defBuff: _defensive.Remove(defBuff); break;
                case HitBuffData offBuff: _offensive.Remove(offBuff); break;
                case StatBuffData statBuff: _statBuffs.Remove(statBuff); break;
                case StatDebuffData statDebuff: _statDebuffs.Remove(statDebuff); break;
            }
        }

        public void Clear()
        {
            _all.Clear();
            _defensive.Clear();
            _offensive.Clear();
            _statBuffs.Clear();
            _statDebuffs.Clear();
        }
    }
}