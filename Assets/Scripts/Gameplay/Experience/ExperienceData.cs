using UniRx;

namespace Gameplay.Experience
{
    public class ExperienceData
    {
        public readonly Subject<int> OnExperienceGained = new();
        public readonly Subject<float> OnProgressChanged = new();
        public readonly Subject<int> OnLevelUp = new();

        public int CurrentExperience { get; private set; }

        public int CurrentLevel { get; private set; } = 1;

        public void AddExperience(int experienceToAdd)
        {
            CurrentExperience += experienceToAdd;
            OnExperienceGained.OnNext(experienceToAdd);
        }

        public void SetProgress(float progress) => OnProgressChanged.OnNext(progress);

        public void IncrementLevel()
        {
            CurrentLevel++;
            OnLevelUp.OnNext(CurrentLevel);
        }
    }
}