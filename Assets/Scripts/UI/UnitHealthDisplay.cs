using Gameplay.Combat.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UnitHealthDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _healthText;

        [Inject] private UnitHealthData _healthData;

        private void Awake()
        {
            SetHealthDisplay();

            _healthData.CurrentHealth.Subscribe(_ => SetHealthDisplay()).AddTo(gameObject);
            _healthData.MaxHealth.Subscribe(_ => SetHealthDisplay()).AddTo(gameObject);
        }

        private void SetHealthDisplay()
        {
            var health = _healthData.CurrentHealth.Value;
            var maxHealth = _healthData.MaxHealth.Value;

            _healthText.text = health + "/" + maxHealth;

            if (maxHealth > 0)
            {
                var fillPercent = health * 1f / maxHealth;
                _slider.value = fillPercent;
            }
        }
    }
}