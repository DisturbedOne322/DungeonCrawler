using Cysharp.Threading.Tasks;

namespace Gameplay.Experience
{
    public class PlayerExperienceService
    {
        private readonly ExperienceData _experienceData;
        private readonly ExperienceRequirementsProvider _experienceRequirementsProvider;
        private readonly PlayerLevelUpController _playerLevelUpController;

        public PlayerExperienceService(ExperienceData experienceData,
            ExperienceRequirementsProvider experienceRequirementsProvider,
            PlayerLevelUpController playerLevelUpController)
        {
            _experienceData = experienceData;
            _experienceRequirementsProvider = experienceRequirementsProvider;
            _playerLevelUpController = playerLevelUpController;
        }

        public async UniTask AddExperience(int experience)
        {
            _experienceData.AddExperience(experience);

            var currentLevel = _experienceData.CurrentLevel;
            var currentExp = _experienceData.CurrentExperience;

            var targetExp = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);

            if (currentExp >= targetExp)
                await _playerLevelUpController.ProcessLevelUp();
        }
    }
}