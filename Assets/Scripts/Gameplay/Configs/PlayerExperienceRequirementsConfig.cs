using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Gameplay/Configs/PlayerExperienceRequirementsConfig")]
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