using System;
using UnityEngine;

namespace Gameplay.Equipment
{
    [Serializable]
    public class EquipmentStatsIncreaseData
    {
        [SerializeField, Min(0)] private int _constitution = 0;
        [SerializeField, Min(0)] private int _strength = 0;
        [SerializeField, Min(0)] private int _dexterity = 0;
        [SerializeField, Min(0)] private int _intelligence = 0;
        [SerializeField, Min(0)] private int _luck = 0;

        public int Constitution => _constitution;
        public int Strength => _strength;
        public int Dexterity => _dexterity;
        public int Intelligence => _intelligence;
        public int Luck => _luck;
    }
}