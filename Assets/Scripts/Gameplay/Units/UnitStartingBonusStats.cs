using System;
using UnityEngine;

namespace Gameplay.Units
{
    [Serializable]
    public class UnitStartingBonusStats
    {
        [SerializeField] [Range(0, 1)] private float _criticalChance;
        [SerializeField] [Min(0)] private float _criticalDamageBonus;
        [SerializeField] [Range(0, 1)] private float _penetrationRatio;
        [SerializeField] [Min(0)] private int _healthRegen;
        [SerializeField] [Min(0)] private int _manaRegen;

        public float CriticalChance => _criticalChance;
        public float CriticalDamageBonus => _criticalDamageBonus;
        public float PenetrationRatio => _penetrationRatio;
        public int HealthRegen => _healthRegen;
        public int ManaRegen => _manaRegen;
    }
}