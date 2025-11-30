using System.Collections.Generic;
using TMPro;
using UI.Core;
using UI.Menus.MenuItemViews;
using UI.Stats;
using UniRx;
using UnityEngine;

namespace UI.Popups
{
    public class LevelUpPopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _pointsLeftText;

        [SerializeField] private UITable _uiTable;
        
        public void SetTable(Dictionary<BaseMenuItemView, StatIncreaseView> table)
        {
            _uiTable.SetTable(table);
        }

        public void SetReactivePointsLeft(ReactiveProperty<int> reactivePoints)
        {
            var maxPoints = reactivePoints.Value;

            reactivePoints.Subscribe(value => { _pointsLeftText.text = value + "/" + maxPoints; }).AddTo(gameObject);
        }
    }
}