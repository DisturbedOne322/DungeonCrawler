using Constants;
using Helpers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class StatIncreaseView : MonoBehaviour
    {
        [SerializeField] private Slider _statSlider;
        [SerializeField] private TextMeshProUGUI _statText;

        public void Initialize(ReactiveProperty<int> statProperty)
        {
            statProperty.Subscribe(UpdateView).AddTo(gameObject);
        }

        private void UpdateView(int stat)
        {
            _statSlider.value = stat * 1f / StatConstants.MaxStatPoints;
            _statText.text = FormattingHelper.FormatStat(stat);
        }
    }
}