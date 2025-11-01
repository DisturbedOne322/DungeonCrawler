using Gameplay.Units;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.UnitData
{
    public class UnitHealthDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _healthText;

        [Inject] private UnitHealthData _healthData;

        private readonly CompositeDisposable _disposables = new();
        
        private void Awake()
        {
            UpdateHealthDisplay();

            _healthData.CurrentHealth.Subscribe(_ => UpdateHealthDisplay()).AddTo(_disposables);
            _healthData.MaxHealth.Subscribe(_ => UpdateHealthDisplay()).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void UpdateHealthDisplay()
        {
            var health = _healthData.CurrentHealth.Value;
            var maxHealth = _healthData.MaxHealth.Value;

            _healthText.text = health + "/" + maxHealth;

            if (maxHealth == 0)
            {
                _slider.value = 0;
                return;
            }
            
            var fillPercent = health * 1f / maxHealth;
            _slider.value = fillPercent;
        }
    }
}