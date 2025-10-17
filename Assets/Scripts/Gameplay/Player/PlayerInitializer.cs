using AssetManagement.AssetProviders.Core;
using Gameplay.Configs;
using Gameplay.Units;

namespace Gameplay.Player
{
    public class PlayerInitializer
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;

        public PlayerInitializer(BaseConfigProvider<GameplayConfig> configProvider)
        {
            _configProvider = configProvider;
        }

        public void InitializePlayer(PlayerUnit player)
        {
            var config = _configProvider.GetConfig<PlayerCharactersDatabase>();

            var data = config.Database[0];
            player.InitializeUnit(data);
        }
    }
}