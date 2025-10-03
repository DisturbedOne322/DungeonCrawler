using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Units
{
    public class PlayerUnit : GameUnit
    {
        [SerializeField] private PlayerMoveAnimator _playerMoveAnimator;
        
        public PlayerMoveAnimator PlayerMoveAnimator => _playerMoveAnimator;
    }
}