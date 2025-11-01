using Gameplay.Combat.Data;
using Gameplay.Units;
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

        public void IncreaseMaxHealth(int delta)
        {
            var oldMaxHealth = Mathf.Max(1, _unitHealthData.MaxHealth.Value);
            var currentHealth = _unitHealthData.CurrentHealth.Value;
            
            var currentHealthRatio = (float)currentHealth / oldMaxHealth;

            var newMaxHealth = oldMaxHealth + delta;
            
            if (newMaxHealth < 0) 
                newMaxHealth = 1;
            
            _unitHealthData.MaxHealth.Value = newMaxHealth;
            _unitHealthData.CurrentHealth.Value = Mathf.Clamp(
                Mathf.RoundToInt(currentHealthRatio * newMaxHealth),
                1, newMaxHealth
            );
        }

        public bool HasEnoughHp(int requestedHp)
        {
            return _unitHealthData.CurrentHealth.Value >= requestedHp;
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

            _unitHealthData.CurrentHealth.Value = Mathf.Min(_unitHealthData.CurrentHealth.Value + amount,
                _unitHealthData.MaxHealth.Value);
        }
    }
}