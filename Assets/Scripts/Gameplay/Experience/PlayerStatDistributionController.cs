using System.Collections.Generic;
using Constants;
using Cysharp.Threading.Tasks;
using Gameplay.Units;
using UI;
using UI.BattleMenu;
using UI.Gameplay;
using UI.Gameplay.Experience;
using UniRx;

namespace Gameplay.Experience
{
    public class PlayerStatDistributionController
    {
        private readonly PlayerUnit _player;
        private readonly PlayerStatDistrubutionController _playerStatDistributionController;
        private readonly PlayerStatDistributionTableBuilder _playerStatDistributionTableBuilder;
        private readonly UIFactory _uiFactory;

        private ReactiveProperty<int> _statPoints;

        public PlayerStatDistributionController(PlayerUnit player, UIFactory uiFactory,
            PlayerStatDistributionTableBuilder playerStatDistributionTableBuilder,
            PlayerStatDistrubutionController playerStatDistributionController)
        {
            _player = player;
            _uiFactory = uiFactory;
            _playerStatDistributionTableBuilder = playerStatDistributionTableBuilder;
            _playerStatDistributionController = playerStatDistributionController;
        }

        public async UniTask DistributeStatPoints()
        {
            _statPoints = new ReactiveProperty<int>(StatConstants.StatPointsPerLevel);

            var popup = _uiFactory.CreatePopup<LevelUpPopup>();

            var playerStats = GetPlayerStats();
            popup.SetTable(_playerStatDistributionTableBuilder.GetStatsTable(playerStats));
            popup.SetReactivePointsLeft(_statPoints);
            popup.ShowPopup().Forget();

            await _playerStatDistributionController.DistributeStats(playerStats, _statPoints);

            await popup.HidePopup();
        }

        private Dictionary<MenuItemData, ReactiveProperty<int>> GetPlayerStats()
        {
            var menuItemsData = _playerStatDistributionTableBuilder.CreateItemDataList(new List<string>
            {
                "Constitution",
                "Strength",
                "Dexterity",
                "Intelligence",
                "Luck"
            });

            Dictionary<MenuItemData, ReactiveProperty<int>> playerStats = new();

            playerStats[menuItemsData[0]] = _player.UnitStatsData.Constitution;
            playerStats[menuItemsData[1]] = _player.UnitStatsData.Strength;
            playerStats[menuItemsData[2]] = _player.UnitStatsData.Dexterity;
            playerStats[menuItemsData[3]] = _player.UnitStatsData.Intelligence;
            playerStats[menuItemsData[4]] = _player.UnitStatsData.Luck;

            return playerStats;
        }
    }
}