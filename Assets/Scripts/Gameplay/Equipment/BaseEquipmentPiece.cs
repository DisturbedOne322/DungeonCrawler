using System.Collections.Generic;
using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.Equipment
{
    public abstract class BaseEquipmentPiece : BaseGameItem
    {
        [SerializeField] [Space] private List<BaseStatusEffectData> _statusEffects;
        [SerializeField] private EquipmentStatsIncreaseData _statsIncreaseData;
        
        public List<BaseStatusEffectData> StatusEffects => _statusEffects;
        public EquipmentStatsIncreaseData StatsIncreaseData => _statsIncreaseData;
    }
}