using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using UnityEngine;
using Zenject;

namespace Gameplay.Combat
{
    public class GameUnit : MonoBehaviour
    {
        private UnitHealthData _healthData;
        private UnitHealthController _unitHealthController;
        private SkillSelectionProvider _skillSelectionProvider;
        private UnitStatsData _unitStatsData;
        private UnitSkillsData _unitSkillsData;
        
        public UnitHealthData HealthData => _healthData;
        public UnitHealthController UnitHealthController => _unitHealthController;
        public SkillSelectionProvider SkillSelectionProvider => _skillSelectionProvider;
        public UnitStatsData UnitStatsData => _unitStatsData;
        public UnitSkillsData UnitSkillsData => _unitSkillsData;

        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController, 
            SkillSelectionProvider skillSelectionProvider,
            UnitStatsData unitStatsData, 
            UnitSkillsData unitSkillsData)
        {
            _healthData = unitHealthData;
            _unitHealthController = unitHealthController;
            _skillSelectionProvider = skillSelectionProvider;
            _unitStatsData = unitStatsData;
            _unitSkillsData = unitSkillsData;
        }

        public void InitializeUnit(UnitData unitData)
        {
            _unitHealthController.UnitHealthData.Initialize(unitData.MaxHp);
            _unitSkillsData.AssignSkills(unitData.SkillSet);
            _unitStatsData.SetStats(unitData.UnitBaseStats);
        }
    }
}