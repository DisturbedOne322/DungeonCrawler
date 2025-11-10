using AssetManagement.AssetProviders;
using Gameplay.Units;

namespace Gameplay.Player
{
    public class PlayerInitializer
    {
        private readonly PlayerCharactersDatabase _charactersDatabase;
        
        public PlayerInitializer(GameplayConfigsProvider configProvider)
        {
            _charactersDatabase = configProvider.GetConfig<PlayerCharactersDatabase>();
        }

        public void InitializePlayer(PlayerUnit player)
        {
            var data = _charactersDatabase.Database[0];
            player.InitializeUnit(data);
        }
    }
}