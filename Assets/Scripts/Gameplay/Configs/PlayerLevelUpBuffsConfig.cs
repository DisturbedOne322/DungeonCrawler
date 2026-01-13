using Data.Constants;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayConfigs + "PlayerLevelUpBuffsConfig")]
    public class PlayerLevelUpBuffsConfig : GameplayConfig
    {
        [SerializeField] [Min(0)] private int _healthBonus;
        [SerializeField] [Min(0)] private int _manaBonus;

        public int HealthBonus => _healthBonus;
        public int ManaBonus => _manaBonus;
    }
}