using UniRx;

namespace Gameplay.Experience
{
    public class ExperienceData
    {
        private int _currentExperience;
        private int _currentLevel = 1;
        
        public int CurrentExperience => _currentExperience;
        public int CurrentLevel => _currentLevel;

        public readonly Subject<int> OnExperienceGained = new();
        public readonly Subject<int> OnLevelUp = new();

        public void AddExperience(int experienceToAdd)
        {
            _currentExperience += experienceToAdd;
            OnExperienceGained.OnNext(experienceToAdd);
        }

        public void IncrementLevel()
        {
            _currentLevel++;
            OnLevelUp.OnNext(_currentLevel);
        }
    }
}