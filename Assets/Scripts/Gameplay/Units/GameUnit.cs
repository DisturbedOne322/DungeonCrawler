using Animations;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Combat.SkillSelection;
using UnityEngine;
using Zenject;

namespace Gameplay.Units
{
    public class GameUnit : MonoBehaviour
    {
        [SerializeField] private EvadeAnimator _evadeAnimator;
        
        private UnitHealthData _healthData;
        private UnitHealthController _unitHealthController;
        private ActionSelectionProvider _actionSelectionProvider;
        private UnitStatsData _unitStatsData;
        private UnitSkillsData _unitSkillsData;
        private UnitBuffsData _unitBuffsData;
        private UnitInventoryData _unitInventoryData;
        
        public UnitHealthData HealthData => _healthData;
        public UnitHealthController UnitHealthController => _unitHealthController;
        public ActionSelectionProvider ActionSelectionProvider => _actionSelectionProvider;
        public UnitStatsData UnitStatsData => _unitStatsData;
        public UnitSkillsData UnitSkillsData => _unitSkillsData;
        public UnitBuffsData UnitBuffsData => _unitBuffsData;
        public UnitInventoryData UnitInventoryData => _unitInventoryData;
        
        public EvadeAnimator EvadeAnimator => _evadeAnimator;

        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController, 
            ActionSelectionProvider actionSelectionProvider,
            UnitStatsData unitStatsData, 
            UnitSkillsData unitSkillsData,
            UnitBuffsData unitBuffsData,
            UnitInventoryData unitInventoryData)
        {
            _healthData = unitHealthData;
            _unitHealthController = unitHealthController;
            _actionSelectionProvider = actionSelectionProvider;
            _unitStatsData = unitStatsData;
            _unitSkillsData = unitSkillsData;
            _unitBuffsData = unitBuffsData;
            _unitInventoryData = unitInventoryData;
        }

        public void InitializeUnit(UnitData unitData)
        {
            _healthData.Initialize(unitData.MaxHp);
            _unitStatsData.SetStats(unitData.UnitBaseStats);
            
            _unitSkillsData.AssignSkills(unitData);
            _unitInventoryData.AssignItems(unitData.Items);
        }
    }
}