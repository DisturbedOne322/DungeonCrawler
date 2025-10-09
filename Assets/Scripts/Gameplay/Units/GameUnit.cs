using System.Collections.Generic;
using Animations;
using Cysharp.Threading.Tasks;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
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
        
        private List<IOffensiveBuff> _offensiveModifiers = new();
        private List<IDefensiveBuff> _defensiveModifiers = new();
        
        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController, 
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
            _unitStatsData.SetStats(unitData.UnitBaseStats);
            
            _unitSkillsData.AssignSkills(unitData);
            _unitInventoryData.AddItems(unitData.Items);
            
            if(unitData.BaseWeapon)
                _unitEquipmentData.EquipWeapon(unitData.BaseWeapon);
            
            if(unitData.BaseArmor)
                _unitEquipmentData.EquipArmor(unitData.BaseArmor);
        }

        public List<IOffensiveBuff> GetOffensiveModifiers()
        {
            _offensiveModifiers.Clear();
            _offensiveModifiers.AddRange(_unitBuffsData.OffensiveBuffs);
            
            TryAddModifiersFromEquipment(_offensiveModifiers);
            
            return _offensiveModifiers;
        }

        public List<IDefensiveBuff> GetDefensiveModifiers()
        {
            _defensiveModifiers.Clear();
            _defensiveModifiers.AddRange(_unitBuffsData.DefensiveBuffs);
            
            TryAddModifiersFromEquipment(_defensiveModifiers);
            
            return _defensiveModifiers;       
        }
        
        private void TryAddModifiersFromEquipment(List<IOffensiveBuff> offensiveModifiers)
        {
            if(_unitEquipmentData.TryGetEquippedWeapon(out var weapon) && weapon.OffensiveBuff)
                offensiveModifiers.Add(weapon.OffensiveBuff);
            
            if(_unitEquipmentData.TryGetEquippedArmor(out var armor) && armor.OffensiveBuff)
                offensiveModifiers.Add(armor.OffensiveBuff);
        }
        
        private void TryAddModifiersFromEquipment(List<IDefensiveBuff> defensiveModifiers)
        {
            if(_unitEquipmentData.TryGetEquippedWeapon(out var weapon) && weapon.DefensiveBuff)
                defensiveModifiers.Add(weapon.DefensiveBuff);
            
            if(_unitEquipmentData.TryGetEquippedArmor(out var armor) && armor.DefensiveBuff)
                defensiveModifiers.Add(armor.DefensiveBuff);
        }
    }
}