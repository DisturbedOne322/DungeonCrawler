using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerCameraMovement : MonoBehaviour
    {
        [Inject]
        private PlayerController _playerController;

        private void Update()
        {
            transform.position = _playerController.transform.position;
            transform.forward = _playerController.transform.forward;
        }
    }
}