using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.OffensiveCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Debuffs;
using UnityEngine;

namespace Gameplay.Equipment
{
    public abstract class BaseEquipmentPiece : BaseGameItem
    {
        [SerializeField] [Space] private List<OffensiveBuffData> _offensiveBuffs;
        [SerializeField] private List<DefensiveBuffData> _defensiveBuffs;
        [SerializeField] private List<StatBuffData> _statBuffs;
        [SerializeField] private List<StatDebuffData> _statDebuffs;

        public List<OffensiveBuffData> OffensiveBuffs => _offensiveBuffs;
        public List<DefensiveBuffData> DefensiveBuffs => _defensiveBuffs;
        public List<StatBuffData> StatBuffs => _statBuffs;
        public List<StatDebuffData> StatDebuffs => _statDebuffs;
    }
}