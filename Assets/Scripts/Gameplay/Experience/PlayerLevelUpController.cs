using System.Collections.Generic;
using Constants;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Gameplay.Units;
using UI;
using UI.BattleMenu;
using UI.Gameplay;
using UniRx;

namespace Gameplay.Experience
{
    public class PlayerLevelUpController
    {
        private readonly PlayerUnit _player;
        private readonly UIFactory _uiFactory;
        private readonly PlayerStatDistributionTableBuilder _playerStatDistributionTableBuilder;
        private readonly PlayerStatDistrubutionController _playerStatDistrubutionController;

        private ReactiveProperty<int> _statPoints;
        
        public PlayerLevelUpController(PlayerUnit player, UIFactory uiFactory, 
            PlayerStatDistributionTableBuilder playerStatDistributionTableBuilder,
            PlayerStatDistrubutionController playerStatDistrubutionController)
        {
            _player = player;
            _uiFactory = uiFactory;
            _playerStatDistributionTableBuilder = playerStatDistributionTableBuilder;
            _playerStatDistrubutionController = playerStatDistrubutionController;
        }

        public async UniTask DistributeStatPoints()
        {
            _statPoints = new(StatConstants.StatPointsPerLevel);
            
            var popup = _uiFactory.CreatePopup<LevelUpPopup>();

            var playerStats = GetPlayerStats();
            popup.SetTable(_playerStatDistributionTableBuilder.GetStatsTable(playerStats));
            popup.SetReactivePointsLeft(_statPoints);
            popup.ShowPopup().Forget();

            await _playerStatDistrubutionController.DistributeStats(playerStats, _statPoints);
            
            await popup.HidePopup();
        }

        private Dictionary<MenuItemData, ReactiveProperty<int>> GetPlayerStats()
        {
            var menuItemsData = _playerStatDistributionTableBuilder.CreateItemDataList(new ()
            {
                "Constitution",
                "Strength",
                "Dexterity",
                "Intelligence",
                "Luck",
            });

            Dictionary<MenuItemData, ReactiveProperty<int>> playerStats = new ();

            playerStats[menuItemsData[0]] = _player.UnitStatsData.Constitution;
            playerStats[menuItemsData[1]] = _player.UnitStatsData.Strength;
            playerStats[menuItemsData[2]] = _player.UnitStatsData.Dexterity;
            playerStats[menuItemsData[3]] = _player.UnitStatsData.Intelligence;
            playerStats[menuItemsData[4]] = _player.UnitStatsData.Luck;
            
            return playerStats;
        }
    }
}