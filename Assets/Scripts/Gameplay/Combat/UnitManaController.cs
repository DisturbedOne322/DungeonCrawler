using Gameplay.Combat.Data;
using Gameplay.Units;
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

        public void IncreaseMaxMana(int delta)
        {
            var currentMana = _unitManaData.CurrentMana.Value;
            var currentMaxMana = Mathf.Max(1, _unitManaData.MaxMana.Value);
            
            var currentManaRatio = (float)currentMana / currentMaxMana;

            currentMaxMana += delta;
            
            _unitManaData.MaxMana.Value = currentMaxMana;
            _unitManaData.CurrentMana.Value = Mathf.Clamp(
                Mathf.RoundToInt(currentManaRatio * currentMaxMana),
                0, currentMaxMana
            );
        }

        public bool HasEnoughMana(int requestedMana)
        {
            return _unitManaData.CurrentMana.Value >= requestedMana;
        }

        public void UseMana(int amount)
        {
            _unitManaData.CurrentMana.Value = Mathf.Max(_unitManaData.CurrentMana.Value - amount, 0);
        }

        public void RegenerateMana(int amount)
        {
            _unitManaData.CurrentMana.Value =
                Mathf.Min(_unitManaData.CurrentMana.Value + amount, _unitManaData.MaxMana.Value);
        }
    }
}