using AssetManagement.AssetProviders.Core;
using Gameplay.Progression;

namespace Gameplay.Experience
{
    public class ExperienceRequirementsProvider
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;

        public ExperienceRequirementsProvider(BaseConfigProvider<GameplayConfig> configProvider)
        {
            _configProvider = configProvider;
        }
        
        public int GetXpRequiredForLevel(int level) => _configProvider.GetConfig<PlayerExperienceRequirementedConfig>().GetXpRequiredForLevel(level);
    }
}