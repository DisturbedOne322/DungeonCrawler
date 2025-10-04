using UnityEngine;

namespace Gameplay.Progression
{
    [CreateAssetMenu(menuName = "Gameplay/Configs/PlayerLevelUpBuffsConfig")]
    public class PlayerLevelUpBuffsConfig : GameplayConfig
    {
        [SerializeField, Min(0)] private int _healthBonus;
        
        public int HealthBonus => _healthBonus;
    }
}