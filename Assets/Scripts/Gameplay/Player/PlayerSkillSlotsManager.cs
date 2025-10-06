using System;
using System.Linq;
using AssetManagement.AssetProviders.Core;
using Gameplay.Experience;
using Gameplay.Progression;
using Gameplay.Units;
using UniRx;

namespace Gameplay.Player
{
    public class PlayerSkillSlotsManager : IDisposable
    {
        private readonly PlayerUnit _playerUnit;
        private readonly ExperienceData _experienceData;
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;

        private readonly IDisposable _disposable;
        
        private int _previousSlots = 0;
        
        public readonly Subject<int> OnSkillSlotsAdded = new ();
        
        public PlayerSkillSlotsManager(PlayerUnit playerUnit,
            ExperienceData experienceData, 
            BaseConfigProvider<GameplayConfig> configProvider)
        {
            _playerUnit = playerUnit;
            _experienceData = experienceData;
            _configProvider = configProvider;
            
            _disposable = experienceData.OnLevelUp.Subscribe(ProcessLevelUp);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public int GetPlayerSkillSlots() => GetSkillSlotsForLevel(_experienceData.CurrentLevel);

        public bool HasFreeSkillSlot() => GetPlayerSkillSlots() > _playerUnit.UnitSkillsData.Skills.Count;

        private void ProcessLevelUp(int level)
        {
            int skillSlots = GetSkillSlotsForLevel(level);
            
            if(_previousSlots == 0)
                _previousSlots = skillSlots;

            if (_previousSlots >= skillSlots) 
                return;

            OnSkillSlotsAdded?.OnNext(skillSlots - _previousSlots);
            _previousSlots = skillSlots;
        }

        private int GetSkillSlotsForLevel(int level)
        {
            var config = _configProvider.GetConfig<PlayerSkillSlotUnlockConfig>();

            int skillSlots = config.StartingSkillSlots;
            skillSlots += config.AdditionalSkillSlotLevelThresholds.Count(threshold => level >= threshold);
            
            return skillSlots;
        }
    }
}