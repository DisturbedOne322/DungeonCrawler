using AssetManagement.AssetProviders;
using Gameplay.Configs;

namespace Gameplay.Experience
{
    public class ExperienceRequirementsProvider
    {
        private readonly PlayerExperienceRequirementsConfig _config;

        public ExperienceRequirementsProvider(GameplayConfigsProvider configProvider)
        {
            _config = configProvider.GetConfig<PlayerExperienceRequirementsConfig>();
        }

        public int GetXpRequiredForLevel(int level)
        {
            return _config.GetXpRequiredForLevel(level);
        }
    }
}