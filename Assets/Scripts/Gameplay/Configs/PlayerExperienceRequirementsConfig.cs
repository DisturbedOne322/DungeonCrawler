using System.Collections.Generic;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayConfigs + "PlayerExperienceRequirementsConfig")]
    public class PlayerExperienceRequirementsConfig : GameplayConfig
    {
        [SerializeField] private List<int> _levelRequirements = new();

        public int GetXpRequiredForLevel(int level)
        {
            level--;

            if (level >= _levelRequirements.Count || level < 0)
                return 0;

            return _levelRequirements[level];
        }
    }
}