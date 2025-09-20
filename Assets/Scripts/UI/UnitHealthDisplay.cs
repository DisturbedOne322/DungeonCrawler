using System;
using Gameplay.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace UI
{
    public class UnitHealthDisplay : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _healthText;
        
        [Inject]
        private UnitHealthData _unitHealthData;

        private void Awake()
        {
            SetHealthDisplay();
            
            _unitHealthData.CurrentHealth.Subscribe(_ => SetHealthDisplay()).AddTo(gameObject);
            _unitHealthData.MaxHealth.Subscribe(_ => SetHealthDisplay()).AddTo(gameObject);
        }

        private void SetHealthDisplay()
        {
            int health = _unitHealthData.CurrentHealth.Value;
            int maxHealth = _unitHealthData.MaxHealth.Value;
            
            float fillPercent = health * 1f / maxHealth;
            _slider.value = fillPercent;
            
            _healthText.text = health + "/" + maxHealth;
        }
    }
}