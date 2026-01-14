using Data.Constants;
using Gameplay.Configs;
using UnityEngine;

namespace Gameplay.Dungeon
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonConfigs + "DungeonRulesConfig")]
    public class DungeonRulesConfig : GameplayConfig
    {
        [SerializeField, Min(1)] private int _minCorridorLength = 4;
        [SerializeField, Min(2)] private int _maxCorridorLength = 8;
        
        [SerializeField, Min(0)] private int _startEncounterOffset = 1;
        [SerializeField, Min(0)] private int _endEncounterOffset = 1;

        public int MinCorridorLength => _minCorridorLength;
        public int MaxCorridorLength => _maxCorridorLength;
        
        public int StartEncounterOffset => _startEncounterOffset;
        public int EndEncounterOffset => _endEncounterOffset;
        
        private void OnValidate()
        {
            if(_minCorridorLength >= _maxCorridorLength)
                _maxCorridorLength = _minCorridorLength + 1;

            if (_startEncounterOffset >= _minCorridorLength) 
                _startEncounterOffset = _minCorridorLength;
            
            if(_startEncounterOffset + _endEncounterOffset >= _minCorridorLength)
                _endEncounterOffset = _minCorridorLength - _startEncounterOffset;
        }
    }
}