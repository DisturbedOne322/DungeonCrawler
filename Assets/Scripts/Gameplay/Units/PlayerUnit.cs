using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Units
{
    public class PlayerUnit : GameUnit
    {
        [SerializeField] private PlayerMoveAnimator _playerMoveAnimator;
        [SerializeField] private Transform _cameraPivot;

        public PlayerMoveAnimator PlayerMoveAnimator => _playerMoveAnimator;
        public Transform CameraPivot => _cameraPivot;
    }
}