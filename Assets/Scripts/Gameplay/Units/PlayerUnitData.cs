using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "Gameplay/Units/PlayerUnitData")]
    public class PlayerUnitData : UnitData
    {
        [SerializeField, Min(0)] private int _startingBalance = 0;
        
        public int StartingBalance => _startingBalance;
    }
}