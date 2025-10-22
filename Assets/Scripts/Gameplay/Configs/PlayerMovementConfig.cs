using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Gameplay/Configs/PlayerMovementConfig")]
    public class PlayerMovementConfig : GameplayConfig
    {
        [SerializeField, Min(0.01f)] private float _moveTimePerMeter = 0.01f;
        
        public float MoveTimePerMeter => _moveTimePerMeter;
    }
}