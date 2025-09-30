using System;
using Gameplay.Combat;
using UniRx;

namespace Gameplay.Experience
{
    public class PlayerExperienceService
    {
        private readonly ExperienceData _experienceData;
        private readonly ExperienceRequirementsProvider _experienceRequirementsProvider;

        
        public PlayerExperienceService(ExperienceData experienceData, 
            ExperienceRequirementsProvider experienceRequirementsProvider)
        {
            _experienceData = experienceData;
            _experienceRequirementsProvider = experienceRequirementsProvider;
        }

        public void AddExperience(int experience)
        {
            _experienceData.AddExperience(experience);

            int currentLevel = _experienceData.CurrentLevel;
            int currentExp = _experienceData.CurrentExperience;
            
            int targetExp = _experienceRequirementsProvider.GetXpRequiredForLevel(currentLevel + 1);
            
            if(currentExp >= targetExp)
                _experienceData.IncrementLevel();
        }
    }
}