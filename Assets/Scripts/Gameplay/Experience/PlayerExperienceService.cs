using System;
using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using UniRx;

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

            int currentLevel = _experienceData.CurrentLevel;
            int currentExp = _experienceData.CurrentExperience;
            
            int targetExp = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);

            if (currentExp >= targetExp)
            {
                _experienceData.IncrementLevel();
                await _playerLevelUpController.DistributeStatPoints();
            }
        }
    }
}