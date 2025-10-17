using System;
using System.Linq;
using AssetManagement.AssetProviders.Core;
using Gameplay.Configs;
using Gameplay.Experience;
using Gameplay.Units;
using UniRx;

namespace Gameplay.Player
{
    public class PlayerSkillSlotsManager : IDisposable
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;

        private readonly IDisposable _disposable;
        private readonly ExperienceData _experienceData;
        private readonly PlayerUnit _playerUnit;

        public readonly Subject<int> OnSkillSlotsAdded = new();

        private int _previousSlots;

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

        public int GetPlayerSkillSlots()
        {
            return GetSkillSlotsForLevel(_experienceData.CurrentLevel);
        }

        public bool HasFreeSkillSlot()
        {
            return GetPlayerSkillSlots() > _playerUnit.UnitSkillsData.Skills.Count;
        }

        private void ProcessLevelUp(int level)
        {
            var skillSlots = GetSkillSlotsForLevel(level);

            if (_previousSlots == 0)
                _previousSlots = skillSlots;

            if (_previousSlots >= skillSlots)
                return;

            OnSkillSlotsAdded?.OnNext(skillSlots - _previousSlots);
            _previousSlots = skillSlots;
        }

        private int GetSkillSlotsForLevel(int level)
        {
            var config = _configProvider.GetConfig<PlayerSkillSlotUnlockConfig>();

            var skillSlots = config.StartingSkillSlots;
            skillSlots += config.AdditionalSkillSlotLevelThresholds.Count(threshold => level >= threshold);

            return skillSlots;
        }
    }
}