using System;
using UnityEngine;

namespace Gameplay.Combat.Data
{
    [Serializable]
    public class UnitBaseStats
    {
        [SerializeField] private int _constitution = 1;
        [SerializeField] private int _strength = 1;
        [SerializeField] private int _dexterity = 1;
        [SerializeField] private int _intelligence = 1;
        [SerializeField] private int _luck = 1;
        
        public int Constitution => _constitution;
        public int Strength => _strength;
        public int Dexterity => _dexterity;
        public int Intelligence => _intelligence;
        public int Luck => _luck;
    }
}