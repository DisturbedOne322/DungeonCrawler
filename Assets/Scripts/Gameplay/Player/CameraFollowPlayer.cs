using System;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        private Transform _playerTransform;
        
        [Inject]
        private void Construct(PlayerUnit playerUnit)
        {
            _playerTransform = playerUnit.transform;
        }

        private void Update()
        {
            if(!_playerTransform)
                return;
            
            transform.position = _playerTransform.position;
            transform.rotation = _playerTransform.rotation;
        }
    }
}