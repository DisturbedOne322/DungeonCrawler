using System.Collections.Generic;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.Debuffs;
using UnityEngine;

namespace Gameplay.Equipment
{
    public abstract class BaseEquipmentPiece : BaseGameItem
    {
        [SerializeField] [Space] private List<BaseStatusEffectData> _statusEffects;

        public List<BaseStatusEffectData> StatusEffects => _statusEffects;
    }
}