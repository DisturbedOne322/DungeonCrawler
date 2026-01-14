using Cysharp.Threading.Tasks;
using Data.Constants;
using UI;
using UI.Popups;
using UniRx;

namespace Gameplay.Services.Stats
{
    public class PlayerStatDistributionController
    {
        private readonly PlayerStatDistrubutionController _playerStatDistributionController;
        private readonly PlayerStatTableBuilder _playerStatTableBuilder;
        private readonly UIFactory _uiFactory;

        private ReactiveProperty<int> _statPoints;

        public PlayerStatDistributionController(UIFactory uiFactory,
            PlayerStatTableBuilder playerStatTableBuilder,
            PlayerStatDistrubutionController playerStatDistributionController)
        {
            _uiFactory = uiFactory;
            _playerStatTableBuilder = playerStatTableBuilder;
            _playerStatDistributionController = playerStatDistributionController;
        }

        public async UniTask DistributeStatPoints()
        {
            _statPoints = new ReactiveProperty<int>(StatConstants.StatPointsPerLevel);

            var popup = _uiFactory.CreatePopup<LevelUpPopup>();

            var menuItems = _playerStatTableBuilder.CreateMenuItems();

            popup.SetTable(_playerStatTableBuilder.GetStatsTable(menuItems));
            popup.SetReactivePointsLeft(_statPoints);
            popup.ShowPopup().Forget();

            await _playerStatDistributionController.DistributeStats(menuItems, _statPoints);

            await popup.HidePopup();
        }
    }
}