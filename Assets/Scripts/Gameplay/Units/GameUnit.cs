using Animations;
using Gameplay.Combat;
using Gameplay.Combat.Data; 
using Gameplay.Combat.SkillSelection;
using Gameplay.Equipment;
using Gameplay.Facades;
using Gameplay.Visual;
using UnityEngine;
using Zenject;

namespace Gameplay.Units
{
    public class GameUnit : MonoBehaviour, IGameUnit
    {
        [SerializeField] private EvadeAnimator _evadeAnimator;
        [SerializeField] private AttackAnimator _attackAnimator;
        
        private UnitHealthData _healthData;
        private UnitHealthController _unitHealthController;
        
        private UnitManaData _manaData;
        private UnitManaController _unitManaController;
        
        private ActionSelectionProvider _actionSelectionProvider;
        private UnitStatsData _unitStatsData;
        private UnitSkillsData _unitSkillsData;
        private UnitBuffsData _unitBuffsData;
        private UnitActiveBuffsData _unitActiveBuffsData;
        private UnitInventoryData _unitInventoryData;
        private UnitEquipmentData _unitEquipmentData;

        private string _unitName;
        
        public Vector3 Position => transform.position;
        
        public UnitHealthData UnitHealthData => _healthData;
        public UnitHealthController UnitHealthController => _unitHealthController;
        public ActionSelectionProvider ActionSelectionProvider => _actionSelectionProvider;
        public UnitStatsData UnitStatsData => _unitStatsData;
        public UnitSkillsData UnitSkillsData => _unitSkillsData;
        public UnitBuffsData UnitBuffsData => _unitBuffsData;
        public UnitActiveBuffsData UnitActiveBuffsData => _unitActiveBuffsData;
        public UnitInventoryData UnitInventoryData => _unitInventoryData;
        public UnitEquipmentData UnitEquipmentData => _unitEquipmentData;

        public EvadeAnimator EvadeAnimator => _evadeAnimator;
        public AttackAnimator AttackAnimator => _attackAnimator;
        
        public string EntityName => _unitName;
        
        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController, 
            UnitManaData unitManaData,
            UnitManaController unitManaController,
            ActionSelectionProvider actionSelectionProvider,
            UnitStatsData unitStatsData, 
            UnitSkillsData unitSkillsData,
            UnitBuffsData unitBuffsData,
            UnitActiveBuffsData unitActiveBuffsData,
            UnitInventoryData unitInventoryData,
            UnitEquipmentData unitEquipmentData)
        {
            _healthData = unitHealthData;
            _unitHealthController = unitHealthController;
            _manaData = unitManaData;
            _unitManaController = unitManaController;
            _actionSelectionProvider = actionSelectionProvider;
            _unitStatsData = unitStatsData;
            _unitSkillsData = unitSkillsData;
            _unitBuffsData = unitBuffsData;
            _unitActiveBuffsData = unitActiveBuffsData;
            _unitInventoryData = unitInventoryData;
            _unitEquipmentData = unitEquipmentData;
        }

        public void InitializeUnit(UnitData unitData)
        {
            _unitName = unitData.Name;
            
            _healthData.Initialize(unitData.MaxHp);
            _manaData.Initialize(unitData.MaxMp);
            
            _unitStatsData.SetStats(unitData.UnitBaseStats);
            
            _unitSkillsData.AssignSkills(unitData);
            _unitInventoryData.AddItems(unitData.Items);
            
            if(unitData.BaseWeapon)
                _unitEquipmentData.EquipWeapon(unitData.BaseWeapon);
            
            if(unitData.BaseArmor)
                _unitEquipmentData.EquipArmor(unitData.BaseArmor);
        }
    }
}