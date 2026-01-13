using Data.Constants;
using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayUnits + "PlayerUnitData")]
    public class PlayerUnitData : UnitData
    {
        [SerializeField] [Min(0)] private int _startingBalance;

        public int StartingBalance => _startingBalance;
    }
}