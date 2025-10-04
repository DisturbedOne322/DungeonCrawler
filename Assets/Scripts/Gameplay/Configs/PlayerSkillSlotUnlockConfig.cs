using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Progression
{
    [CreateAssetMenu(fileName = "PlayerProgressionConfig", menuName = "Gameplay/Configs/PlayerProgressionConfig")]
    public class PlayerSkillSlotUnlockConfig : GameplayConfig
    {
        [SerializeField] private int _startingSkillSlots = 3;
        [SerializeField] private List<int> _additionalSkillSlotLevelThresholds;
        
        public int StartingSkillSlots => _startingSkillSlots;
        public List<int> AdditionalSkillSlotLevelThresholds => _additionalSkillSlotLevelThresholds;
    }
}