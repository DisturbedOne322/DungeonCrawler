using System.Collections.Generic;
using System.Linq;
using Constants;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using UI.BattleMenu;
using UI.Stats;
using UniRx;
using UnityEngine;

namespace Gameplay.Experience
{
    public class PlayerStatDistrubutionController
    {
        private readonly PlayerInputProvider _playerInputProvider;
        
        private StatDistributionMenuUpdater _statDistributionMenuUpdater;
        
        private CompositeDisposable _disposables;

        private List<int> _originalStats;
        private List<ReactiveProperty<int>> _currentStats;
        
        private ReactiveProperty<int> _statPoints;
        
        public PlayerStatDistrubutionController(PlayerInputProvider playerInputProvider)
        {
            _playerInputProvider = playerInputProvider;
        }
        
        public async UniTask DistributeStats(Dictionary<MenuItemData, ReactiveProperty<int>> playerStats, ReactiveProperty<int> statPoints)
        {
            _statPoints = statPoints;
            
            InitializeStatsData(playerStats.Values.ToList());
            InitializeMenu(playerStats.Keys.ToList());
            SubscribeToInputEvents();
            
            _playerInputProvider.EnableUiInput(true);

            await UniTask.WaitUntil(() => _statPoints.Value == 0);
            
            _playerInputProvider.EnableUiInput(false);
            Dispose();
        }

        private void InitializeStatsData(List<ReactiveProperty<int>> stats)
        {
            _originalStats = new ();
            _currentStats = new();
            
            foreach (var statValue in stats)
            {
                _originalStats.Add(statValue.Value);
                _currentStats.Add(statValue);
            }
        }

        private void InitializeMenu(List<MenuItemData> menuItems)
        {
            _statDistributionMenuUpdater = new StatDistributionMenuUpdater();
            _statDistributionMenuUpdater.SetMenuItems(menuItems);
            _statDistributionMenuUpdater.ResetSelection();
        }

        private void SubscribeToInputEvents()
        {
            _disposables = new();
            _disposables.Add(_playerInputProvider.OnUiDown.Subscribe(_ => _statDistributionMenuUpdater.UpdateSelection(+1)));
            _disposables.Add(_playerInputProvider.OnUiUp.Subscribe(_ => _statDistributionMenuUpdater.UpdateSelection(-1)));
            _disposables.Add(_playerInputProvider.OnUiLeft.Subscribe(_ => TryIncreaseStat(-1)));
            _disposables.Add(_playerInputProvider.OnUiRight.Subscribe(_ => TryIncreaseStat(+1)));
        }

        private void Dispose() => _disposables.Dispose();

        private void TryIncreaseStat(int increment)
        {
            int selectedIndex = _statDistributionMenuUpdater.GetSelectedIndex();
            
            if(increment > 0 && _currentStats[selectedIndex].Value >= StatConstants.MaxStatPoints)
                return;
            
            if(increment < 0 && _currentStats[selectedIndex].Value <= _originalStats[selectedIndex])
                return;
            
            _currentStats[selectedIndex].Value += increment;
            _statPoints.Value -= increment;
        }
    }
}