using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Gameplay/Configs/PlayerExperienceRequirementsConfig")]
    public class PlayerExperienceRequirementsConfig : GameplayConfig
    {
        private const int BaseExperience = 100;
        private const float BaseValue = 1.1f;

        public int GetXpRequiredForLevel(int level)
        {
            return (int)(BaseExperience * Mathf.Pow(BaseValue, level - 1));
        }
    }
}