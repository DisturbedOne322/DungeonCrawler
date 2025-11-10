using System;
using UnityEngine;

namespace Gameplay.Units
{
    [Serializable]
    public class UnitStartingStats
    {
        [SerializeField] [Min(1)] private int _constitution = 1;
        [SerializeField] [Min(1)] private int _strength = 1;
        [SerializeField] [Min(1)] private int _dexterity = 1;
        [SerializeField] [Min(1)] private int _intelligence = 1;
        [SerializeField] [Min(1)] private int _luck = 1;

        public int Constitution => _constitution;
        public int Strength => _strength;
        public int Dexterity => _dexterity;
        public int Intelligence => _intelligence;
        public int Luck => _luck;
    }
}