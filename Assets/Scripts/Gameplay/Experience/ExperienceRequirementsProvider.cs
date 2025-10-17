using AssetManagement.AssetProviders.Core;
using Gameplay.Configs;

namespace Gameplay.Experience
{
    public class ExperienceRequirementsProvider
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;

        public ExperienceRequirementsProvider(BaseConfigProvider<GameplayConfig> configProvider)
        {
            _configProvider = configProvider;
        }

        public int GetXpRequiredForLevel(int level)
        {
            return _configProvider.GetConfig<PlayerExperienceRequirementsConfig>().GetXpRequiredForLevel(level);
        }
    }
}