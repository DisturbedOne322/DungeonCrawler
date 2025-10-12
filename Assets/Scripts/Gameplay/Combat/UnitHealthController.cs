using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat
{
    public class UnitHealthController
    {
        private readonly UnitHealthData _unitHealthData;
        
        public UnitHealthController(UnitHealthData unitHealthData)
        {
            _unitHealthData = unitHealthData;
        }
        
        public void SetNewMaxHealth(int maxHealth)
        {
            maxHealth = Mathf.Max(1, maxHealth);
            
            int currentHealth = _unitHealthData.CurrentHealth.Value;
            int currentMaxHealth = Mathf.Max(1, _unitHealthData.MaxHealth.Value);
            
            float currentHealthRatio = (float)currentHealth / currentMaxHealth;
            
            _unitHealthData.MaxHealth.Value = maxHealth;
            _unitHealthData.CurrentHealth.Value = Mathf.Clamp(
                Mathf.RoundToInt(currentHealthRatio * maxHealth), 
                0, maxHealth
            );
        }
        
        public bool HasEnoughHp(int requestedHp) => _unitHealthData.CurrentHealth.Value >= requestedHp;
        
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