using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Combat.SkillSelection;
using UnityEngine;
using Zenject;

namespace Gameplay.Units
{
    public class GameUnit : MonoBehaviour
    {
        private UnitHealthData _healthData;
        private UnitHealthController _unitHealthController;
        private SkillSelectionProvider _skillSelectionProvider;
        private UnitStatsData _unitStatsData;
        private UnitSkillsData _unitSkillsData;
        private UnitBuffsData _unitBuffsData;
        
        public UnitHealthData HealthData => _healthData;
        public UnitHealthController UnitHealthController => _unitHealthController;
        public SkillSelectionProvider SkillSelectionProvider => _skillSelectionProvider;
        public UnitStatsData UnitStatsData => _unitStatsData;
        public UnitSkillsData UnitSkillsData => _unitSkillsData;
        public UnitBuffsData UnitBuffsData => _unitBuffsData;

        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController, 
            SkillSelectionProvider skillSelectionProvider,
            UnitStatsData unitStatsData, 
            UnitSkillsData unitSkillsData,
            UnitBuffsData unitBuffsData)
        {
            _healthData = unitHealthData;
            _unitHealthController = unitHealthController;
            _skillSelectionProvider = skillSelectionProvider;
            _unitStatsData = unitStatsData;
            _unitSkillsData = unitSkillsData;
            _unitBuffsData = unitBuffsData;
        }

        public void InitializeUnit(UnitData unitData)
        {
            _unitHealthController.UnitHealthData.Initialize(unitData.MaxHp);
            _unitSkillsData.AssignSkills(unitData);
            _unitStatsData.SetStats(unitData.UnitBaseStats);
        }
    }
}