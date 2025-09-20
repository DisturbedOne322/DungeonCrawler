using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using UnityEngine;
using Zenject;

namespace Gameplay.Combat
{
    public class GameUnit : MonoBehaviour
    {
        private UnitHealthController _unitHealthController;
        private SkillSelectionProvider _skillSelectionProvider;
        private UnitStatsData _unitStatsData;
        private UnitSkillsData _unitSkillsData;
        
        public UnitHealthController UnitHealthController => _unitHealthController;
        public SkillSelectionProvider SkillSelectionProvider => _skillSelectionProvider;

        [Inject]
        private void Construct(UnitHealthController unitHealthController, SkillSelectionProvider skillSelectionProvider,
            UnitStatsData unitStatsData, UnitSkillsData unitSkillsData)
        {
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