using System.Collections.Generic;
using Gameplay.Services.Stats;
using UI.Core;
using UI.Menus.MenuItemViews;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace UI.InventoryDisplay
{
    public class PlayerStatsMenuView : BaseDisplayMenuView
    {
        [SerializeField] private UITable _uiTable;

        private PlayerStatTableBuilder _playerStatTableBuilder;

        [Inject]
        private void Construct(PlayerStatTableBuilder playerStatTableBuilder)
        {
            _playerStatTableBuilder = playerStatTableBuilder;
        }

        protected override void Initialize()
        {
            SetStats(_playerStatTableBuilder.GetStatsTable(_playerStatTableBuilder.CreateMenuItems()));
        }

        private void SetStats(Dictionary<BaseMenuItemView, StatIncreaseView> statsTable)
        {
            _uiTable.SetTable(statsTable); 
        }
    }
}