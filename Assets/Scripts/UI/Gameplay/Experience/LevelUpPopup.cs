using System.Collections.Generic;
using TMPro;
using UI.BattleMenu;
using UI.Core;
using UI.Stats;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class LevelUpPopup : BasePopup
    {
        [SerializeField] private VerticalLayoutGroup _namesParent;
        [SerializeField] private VerticalLayoutGroup _statsParent;
        [SerializeField] private TextMeshProUGUI _pointsLeftText;

        public void SetTable(Dictionary<BaseMenuItemView, StatIncreaseView> table)
        {
            foreach (var kv in table)
            {
                kv.Key.transform.SetParent(_namesParent.transform, false);
                kv.Value.transform.SetParent(_statsParent.transform, false);
            }
        }

        public void SetReactivePointsLeft(ReactiveProperty<int> reactivePoints)
        {
            var maxPoints = reactivePoints.Value;

            reactivePoints.Subscribe(value => { _pointsLeftText.text = value + "/" + maxPoints; }).AddTo(gameObject);
        }
    }
}