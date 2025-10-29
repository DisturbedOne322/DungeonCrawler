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
            int currentLevel = _experienceData.CurrentLevel;
            int currentXp = _experienceData.CurrentExperience + experience;

            _experienceData.AddExperience(experience);

            while (true)
            {
                int nextLevelReq = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);

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
            int currentLevel = _experienceData.CurrentLevel;
            int xp = _experienceData.CurrentExperience;

            int currentLevelReq = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel);
            int nextLevelReq = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);

            float progress = (xp - currentLevelReq) / (float)(nextLevelReq - currentLevelReq);
            return Mathf.Clamp01(progress);
        }
    }
}