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

            playerStats[starNames[0]] = _player.UnitStatsData.Constitution;
            playerStats[starNames[1]] = _player.UnitStatsData.Strength;
            playerStats[starNames[2]] = _player.UnitStatsData.Dexterity;
            playerStats[starNames[3]] = _player.UnitStatsData.Intelligence;
            playerStats[starNames[4]] = _player.UnitStatsData.Luck;

            return playerStats;
        }
    }
}