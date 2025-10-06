using DG.Tweening;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private float _followSmoothTime = 0.1f;
        
        private Transform _playerTransform;
        private Vector3 _velocity;

        [Inject]
        private void Construct(PlayerUnit playerUnit)
        {
            _playerTransform = playerUnit.CameraPivot;
        }

        private void LateUpdate()
        {
            if (!_playerTransform)
                return;

            transform.position = _playerTransform.position;
            transform.rotation = _playerTransform.rotation;
            
            return;
            
            transform.position = Vector3.SmoothDamp(
                transform.position,
                _playerTransform.position,
                ref _velocity,
                _followSmoothTime
            );
        }
    }
}