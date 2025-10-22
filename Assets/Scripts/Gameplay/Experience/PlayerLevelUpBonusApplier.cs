using AssetManagement.AssetProviders.Core;
using Gameplay.Configs;
using Gameplay.Units;

namespace Gameplay.Experience
{
    public class PlayerLevelUpBonusApplier
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;
        private readonly PlayerUnit _player;

        public PlayerLevelUpBonusApplier(PlayerUnit player, BaseConfigProvider<GameplayConfig> configProvider)
        {
            _player = player;
            _configProvider = configProvider;
        }

        public void ApplyLevelUpBonus()
        {
            var config = _configProvider.GetConfig<PlayerLevelUpBuffsConfig>();
            var healthBonus = config.HealthBonus;
            var manaBonus = config.ManaBonus;
            
            _player.UnitHealthController.IncreaseMaxHealth(healthBonus);
            _player.UnitManaController.IncreaseMaxMana(manaBonus);
        }
    }
}