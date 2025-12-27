using System;
using UnityEngine;

namespace Gameplay.Units
{
    [Serializable]
    public class UnitStartingBonusStats
    {
        [SerializeField] [Range(0, 1)] private float _criticalChance = 0;
        [SerializeField] [Min(0)] private float _criticalDamageBonus = 0;
        [SerializeField] [Range(0, 1)] private float _penetrationRatio = 0;
        [SerializeField] [Min(0)] private int _healthRegen = 0;
        [SerializeField] [Min(0)] private int _manaRegen = 0;

        public float CriticalChance => _criticalChance;
        public float CriticalDamageBonus => _criticalDamageBonus;
        public float PenetrationRatio => _penetrationRatio;
        public int HealthRegen => _healthRegen;
        public int ManaRegen => _manaRegen;
    }
}