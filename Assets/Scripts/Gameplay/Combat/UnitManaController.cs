using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat
{
    public class UnitManaController
    {
        private readonly UnitManaData _unitManaData;
        
        public UnitManaController(UnitManaData unitManaData)
        {
            _unitManaData = unitManaData;
        }
        
        public void SetNewMaxMana(int maxMana)
        {
            maxMana = Mathf.Max(1, maxMana);
            
            int currentMana = _unitManaData.CurrentMana.Value;
            int currentMaxMana = Mathf.Max(1, _unitManaData.MaxMana.Value);
            
            float currentManaRatio = (float)currentMana / currentMaxMana;
            
            _unitManaData.MaxMana.Value = maxMana;
            _unitManaData.CurrentMana.Value = Mathf.Clamp(
                Mathf.RoundToInt(currentManaRatio * maxMana), 
                0, maxMana
            );
        }
        
        public bool HasEnoughMana(int requestedMana) => _unitManaData.CurrentMana.Value >= requestedMana;
        
        public void UseMana(int amount)
        {
            _unitManaData.CurrentMana.Value = Mathf.Max(_unitManaData.CurrentMana.Value - amount, 0);
        }

        public void RegenerateMana(int amount)
        {
            _unitManaData.CurrentMana.Value = Mathf.Min(_unitManaData.CurrentMana.Value + amount, _unitManaData.MaxMana.Value);
        }
    }
}