using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(fileName = "PlayerUnitData", menuName = "Gameplay/Units/PlayerUnitData")]
    public class PlayerUnitData : UnitData
    {
        [SerializeField] [Min(0)] private int _startingBalance;

        public int StartingBalance => _startingBalance;
    }
}