using System.Collections.Generic;
using Constants;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Gameplay.Units;
using UI;
using UI.Gameplay;
using UniRx;

namespace Gameplay.Experience
{
    public class PlayerLevelUpController
    {
        private readonly PlayerUnit _player;
        private readonly UIFactory _uiFactory;
        private readonly PlayerStatDistributionTableBuilder _playerStatDistributionTableBuilder;

        private ReactiveProperty<int> _statPoints;
        
        public PlayerLevelUpController(PlayerUnit player, UIFactory uiFactory, 
            PlayerStatDistributionTableBuilder playerStatDistributionTableBuilder)
        {
            _player = player;
            _uiFactory = uiFactory;
            _playerStatDistributionTableBuilder = playerStatDistributionTableBuilder;
        }

        public async UniTask DistributeStatPoints()
        {
            _statPoints = new(StatConstants.StatPointsPerLevel);
            
            var popup = _uiFactory.CreatePopup<LevelUpPopup>();
            popup.SetTable(_playerStatDistributionTableBuilder.GetStatsTable(GetPlayerStats()));
            popup.SetReactivePointsLeft(_statPoints);
            popup.ShowPopup();

            await popup.OnDestroyAsync();
        }

        private Dictionary<string, ReactiveProperty<int>> GetPlayerStats()
        {
            Dictionary<string, ReactiveProperty<int>> playerStats = new ();

            playerStats["Constitution"] = _player.UnitStatsData.Constitution;
            playerStats["Strength"] = _player.UnitStatsData.Strength;
            playerStats["Dexterity"] = _player.UnitStatsData.Dexterity;
            playerStats["Intelligence"] = _player.UnitStatsData.Intelligence;
            playerStats["Luck"] = _player.UnitStatsData.Luck;
            
            return playerStats;
        }
    }
}