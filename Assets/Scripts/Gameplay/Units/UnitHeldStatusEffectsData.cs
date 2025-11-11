using System.Collections.Generic;
using Extensions;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.Debuffs.Core;
using UniRx;
using UnityEngine;

namespace Gameplay.Units
{
    public class UnitHeldStatusEffectsData
    {
        private readonly UnitActiveStatusEffectsData _activeStatusEffects;

        private readonly List<DefensiveBuffData> _defensive = new();
        private readonly List<HitBuffData> _offensive = new();
        private readonly List<StatBuffData> _statBuffs = new();
        private readonly List<StatDebuffData> _statDebuffs = new();

        public UnitHeldStatusEffectsData(UnitActiveStatusEffectsData activeStatusEffects)
        {
            _activeStatusEffects = activeStatusEffects;
        }

        public ReactiveCollection<BaseStatusEffectData> All { get; } = new();

        public IReadOnlyList<DefensiveBuffData> DefensiveBuffs => _defensive;
        public IReadOnlyList<HitBuffData> OffensiveBuffs => _offensive;
        public IReadOnlyList<StatBuffData> StatBuffs => _statBuffs;
        public IReadOnlyList<StatDebuffData> StatDebuffs => _statDebuffs;

        public void AssignStartingStatusEffects(List<BaseStatusEffectData> statusEffects)
        {
            foreach (var effectData in statusEffects) 
                Add(effectData);
        }
        
        public void Add(BaseStatusEffectData data)
        {
            if (!data)
                return;

            All.Add(data);

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

            All.Remove(data);

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
            All.Clear();
            _defensive.Clear();
            _offensive.Clear();
            _statBuffs.Clear();
            _statDebuffs.Clear();
        }
    }
}