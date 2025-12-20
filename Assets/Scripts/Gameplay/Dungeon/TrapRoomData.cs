using System;
using System.Collections.Generic;
using Gameplay.StatusEffects.Debuffs.Core;
using UnityEngine;

namespace Gameplay.Dungeon
{
    [Serializable]
    public class TrapRoomData
    {
        [Min(0)] public int MinDamage;
        [Min(1)] public int MaxDamage;
        [Range(0,1f)] public float DamageChance = 0.5f;
        public List<StatDebuffData> Debuffs = new();
    }
}