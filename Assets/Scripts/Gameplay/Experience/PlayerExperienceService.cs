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
            var currentLevel = _experienceData.CurrentLevel;
            var nextExp = _experienceData.CurrentExperience + experience;

            var targetExp = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);

            if (nextExp >= targetExp)
                await _playerLevelUpController.ProcessLevelUp();
            
            _experienceData.AddExperience(experience);
        }
    }
}