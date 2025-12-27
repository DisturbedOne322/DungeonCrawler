using System.Collections.Generic;
using Gameplay.Units;
using UniRx;

namespace Gameplay.Services.Stats
{
    public class PlayerStatsProvider
    {
        private readonly PlayerUnit _player;

        public PlayerStatsProvider(PlayerUnit player)
        {
            _player = player;
        }
        
        public Dictionary<string, ReactiveProperty<int>> GetPlayerStats()
        {
            var starNames = new List<string>
            {
                "Constitution",
                "Strength",
                "Dexterity",
                "Intelligence",
                "Luck"
            };

            Dictionary<string, ReactiveProperty<int>> playerStats = new();

            playerStats[starNames[0]] = _player.UnitStatsData.ConstitutionProp;
            playerStats[starNames[1]] = _player.UnitStatsData.StrengthProp;
            playerStats[starNames[2]] = _player.UnitStatsData.DexterityProp;
            playerStats[starNames[3]] = _player.UnitStatsData.IntelligenceProp;
            playerStats[starNames[4]] = _player.UnitStatsData.LuckProp;

            return playerStats;
        }
    }
}