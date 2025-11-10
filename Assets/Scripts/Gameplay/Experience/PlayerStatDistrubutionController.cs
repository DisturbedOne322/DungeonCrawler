using System.Collections.Generic;
using System.Linq;
using Constants;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using UI.BattleMenu;
using UniRx;

namespace Gameplay.Experience
{
    public class PlayerStatDistrubutionController : BaseUIInputHandler
    {
        private readonly PlayerInputProvider _playerInputProvider;
        private List<ReactiveProperty<int>> _currentStats;

        private CompositeDisposable _disposables;

        private MenuItemsUpdater _menuItemsUpdater;

        private List<int> _originalStats;

        private ReactiveProperty<int> _statPoints;

        public PlayerStatDistrubutionController(PlayerInputProvider playerInputProvider)
        {
            _playerInputProvider = playerInputProvider;
        }

        public async UniTask DistributeStats(Dictionary<MenuItemData, ReactiveProperty<int>> playerStats,
            ReactiveProperty<int> statPoints)
        {
            _statPoints = statPoints;

            InitializeStatsData(playerStats.Values.ToList());
            InitializeMenu(playerStats.Keys.ToList());

            await _playerInputProvider.EnableUIInputUntil(UniTask.WaitUntil(() => _statPoints.Value == 0), this);
        }

        private void InitializeStatsData(List<ReactiveProperty<int>> stats)
        {
            _originalStats = new List<int>();
            _currentStats = new List<ReactiveProperty<int>>();

            foreach (var statValue in stats)
            {
                _originalStats.Add(statValue.Value);
                _currentStats.Add(statValue);
            }
        }

        private void InitializeMenu(List<MenuItemData> menuItems)
        {
            _menuItemsUpdater = new MenuItemsUpdater();
            _menuItemsUpdater.SetMenuItems(menuItems);
        }

        public override void OnUIDown()
        {
            _menuItemsUpdater.UpdateSelection(+1);
        }

        public override void OnUIUp()
        {
            _menuItemsUpdater.UpdateSelection(-1);
        }

        public override void OnUILeft()
        {
            TryIncreaseStat(-1);
        }

        public override void OnUIRight()
        {
            TryIncreaseStat(+1);
        }

        private void TryIncreaseStat(int increment)
        {
            var selectedIndex = _menuItemsUpdater.GetSelectedIndex();

            if (increment > 0 && _currentStats[selectedIndex].Value >= StatConstants.MaxStatPoints)
                return;

            if (increment < 0 && _currentStats[selectedIndex].Value <= _originalStats[selectedIndex])
                return;

            _currentStats[selectedIndex].Value += increment;
            _statPoints.Value -= increment;
        }
    }
}