using System.Collections.Generic;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Configs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayConfigs + "PlayerProgressionConfig")]
    public class PlayerSkillSlotUnlockConfig : GameplayConfig
    {
        [SerializeField, Min(1)] private int _startingSkillSlots = 3;
        [SerializeField] private List<int> _additionalSkillSlotLevelThresholds;

        public int StartingSkillSlots => _startingSkillSlots;
        public List<int> AdditionalSkillSlotLevelThresholds => _additionalSkillSlotLevelThresholds;
    }
}