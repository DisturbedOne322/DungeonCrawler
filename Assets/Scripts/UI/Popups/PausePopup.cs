using System.Collections.Generic;
using UI.BattleMenu;
using UI.Core;
using UI.Menus.MenuItemViews;
using UI.Stats;
using UnityEngine;

namespace UI.Popups
{
    public class PausePopup : BasePopup
    {
        [SerializeField] private UITable _uiTable;

        public void SetStats(Dictionary<BaseMenuItemView, StatIncreaseView> statsTable)
        {
           _uiTable.SetTable(statsTable); 
        }
    }
}