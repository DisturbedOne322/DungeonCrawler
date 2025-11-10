using System;
using UnityEngine;

namespace Gameplay.Equipment
{
    [Serializable]
    public class EquipmentStatsIncreaseData
    {
        [SerializeField] [Min(0)] private int _constitution;
        [SerializeField] [Min(0)] private int _strength;
        [SerializeField] [Min(0)] private int _dexterity;
        [SerializeField] [Min(0)] private int _intelligence;
        [SerializeField] [Min(0)] private int _luck;

        public int Constitution => _constitution;
        public int Strength => _strength;
        public int Dexterity => _dexterity;
        public int Intelligence => _intelligence;
        public int Luck => _luck;
    }
}