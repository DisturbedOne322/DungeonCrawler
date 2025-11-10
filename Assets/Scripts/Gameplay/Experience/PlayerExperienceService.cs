using Cysharp.Threading.Tasks;
using UnityEngine;

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
            var currentXp = _experienceData.CurrentExperience + experience;

            _experienceData.AddExperience(experience);

            while (true)
            {
                var nextLevelReq = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);

                if (currentXp < nextLevelReq)
                    break;

                _experienceData.SetProgress(0);

                await _playerLevelUpController.ProcessLevelUp();

                _experienceData.SetProgress(1);
                currentLevel++;
            }

            _experienceData.SetProgress(CalculateExperienceProgress());
        }

        private float CalculateExperienceProgress()
        {
            var currentLevel = _experienceData.CurrentLevel;
            var xp = _experienceData.CurrentExperience;

            var currentLevelReq = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel);
            var nextLevelReq = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);

            var progress = (xp - currentLevelReq) / (float)(nextLevelReq - currentLevelReq);
            return Mathf.Clamp01(progress);
        }
    }
}