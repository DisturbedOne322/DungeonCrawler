using System.Collections.Generic;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
using Gameplay.Buffs.StatBuffsCore;
using UnityEngine;

namespace Gameplay.Equipment
{
    public abstract class BaseEquipmentPiece : BaseGameItem
    {
        [SerializeField, Space] private List<OffensiveBuffData> _offensiveBuffs;
        [SerializeField] private List<DefensiveBuffData> _defensiveBuffs;
        [SerializeField] private List<StatBuffData> _statBuffs;
        
        public List<OffensiveBuffData> OffensiveBuffs => _offensiveBuffs;
        public List<DefensiveBuffData> DefensiveBuffs => _defensiveBuffs;
        public List<StatBuffData> StatBuffs => _statBuffs;
    }
}