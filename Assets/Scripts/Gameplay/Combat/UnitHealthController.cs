using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat
{
    public class UnitHealthController
    {
        private readonly UnitHealthData _unitHealthData;
        private readonly UnitStatsData _unitStatsData;
        
        public UnitHealthData UnitHealthData => _unitHealthData;

        public UnitHealthController(UnitHealthData unitHealthData, UnitStatsData unitStatsData)
        {
            _unitHealthData = unitHealthData;
            _unitStatsData = unitStatsData;
        }
        
        public void SetNewMaxHealth(int maxHealth)
        {
            int currentHealth = _unitHealthData.CurrentHealth.Value;
            int currentMaxHealth = _unitHealthData.MaxHealth.Value;
            
            float currentHealthRatio = (float)currentHealth / currentMaxHealth;

            _unitHealthData.MaxHealth.Value = maxHealth;
            _unitHealthData.CurrentHealth.Value = Mathf.CeilToInt(currentHealthRatio * maxHealth);
        }
        
        public void TakeDamage(int amount)
        {
            if (_unitHealthData.IsDead.Value) 
                return;
            
            _unitHealthData.CurrentHealth.Value = Mathf.Max(_unitHealthData.CurrentHealth.Value - amount, 0);
        }

        public void Heal(int amount)
        {
            if (_unitHealthData.IsDead.Value) 
                return;
            
            _unitHealthData.CurrentHealth.Value = Mathf.Min(_unitHealthData.CurrentHealth.Value + amount, _unitHealthData.MaxHealth.Value);
        }
    }
}