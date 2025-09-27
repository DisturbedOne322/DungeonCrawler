using System.Collections.Generic;
using Animations;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Combat.Modifiers;
using Gameplay.Combat.SkillSelection;
using Gameplay.Equipment;
using Gameplay.Facades;
using UnityEngine;
using Zenject;

namespace Gameplay.Units
{
    public class GameUnit : MonoBehaviour, IGameUnit
    {
        [SerializeField] private EvadeAnimator _evadeAnimator;
        
        private UnitHealthData _healthData;
        private UnitHealthController _unitHealthController;
        private ActionSelectionProvider _actionSelectionProvider;
        private UnitStatsData _unitStatsData;
        private UnitSkillsData _unitSkillsData;
        private UnitBuffsData _unitBuffsData;
        private UnitInventoryData _unitInventoryData;
        private UnitEquipmentData _unitEquipmentData;
        
        public Vector3 Position => transform.position;
        
        public UnitHealthData UnitHealthData => _healthData;
        public UnitHealthController UnitHealthController => _unitHealthController;
        public ActionSelectionProvider ActionSelectionProvider => _actionSelectionProvider;
        public UnitStatsData UnitStatsData => _unitStatsData;
        public UnitSkillsData UnitSkillsData => _unitSkillsData;
        public UnitBuffsData UnitBuffsData => _unitBuffsData;
        public UnitInventoryData UnitInventoryData => _unitInventoryData;
        public UnitEquipmentData UnitEquipmentData => _unitEquipmentData;

        public EvadeAnimator EvadeAnimator => _evadeAnimator;
        
        private List<IOffensiveModifier> _offensiveModifiers = new();
        private List<IDefensiveModifier> _defensiveModifiers = new();
        
        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController, 
            ActionSelectionProvider actionSelectionProvider,
            UnitStatsData unitStatsData, 
            UnitSkillsData unitSkillsData,
            UnitBuffsData unitBuffsData,
            UnitInventoryData unitInventoryData,
            UnitEquipmentData unitEquipmentData)
        {
            _healthData = unitHealthData;
            _unitHealthController = unitHealthController;
            _actionSelectionProvider = actionSelectionProvider;
            _unitStatsData = unitStatsData;
            _unitSkillsData = unitSkillsData;
            _unitBuffsData = unitBuffsData;
            _unitInventoryData = unitInventoryData;
            _unitEquipmentData = unitEquipmentData;
        }

        public void InitializeUnit(UnitData unitData)
        {
            _healthData.Initialize(unitData.MaxHp);
            _unitStatsData.SetStats(unitData.UnitBaseStats);
            
            _unitSkillsData.AssignSkills(unitData);
            _unitInventoryData.AddItems(unitData.Items);
            
            if(unitData.BaseWeapon)
                _unitEquipmentData.EquipWeapon(unitData.BaseWeapon);
            
            if(unitData.BaseArmor)
                _unitEquipmentData.EquipArmor(unitData.BaseArmor);
        }

        public List<IOffensiveModifier> GetOffensiveModifiers()
        {
            _offensiveModifiers.Clear();
            _offensiveModifiers.AddRange(_unitBuffsData.OffensiveBuffs);
            
            TryAddModifiersFromEquipment(_offensiveModifiers);
            
            return _offensiveModifiers;
        }

        public List<IDefensiveModifier> GetDefensiveModifiers()
        {
            _defensiveModifiers.Clear();
            _defensiveModifiers.AddRange(_unitBuffsData.DefensiveBuffs);
            
            TryAddModifiersFromEquipment(_defensiveModifiers);
            
            return _defensiveModifiers;       
        }

        private void TryAddModifiersFromEquipment(List<IOffensiveModifier> offensiveModifiers)
        {
            if(_unitEquipmentData.TryGetEquippedWeapon(out var weapon) && weapon.OffensiveModifier)
                offensiveModifiers.Add(weapon.OffensiveModifier);
            
            if(_unitEquipmentData.TryGetEquippedArmor(out var armor) && armor.OffensiveModifier)
                offensiveModifiers.Add(armor.OffensiveModifier);
        }
        
        private void TryAddModifiersFromEquipment(List<IDefensiveModifier> defensiveModifiers)
        {
            if(_unitEquipmentData.TryGetEquippedWeapon(out var weapon) && weapon.DefensiveModifier)
                defensiveModifiers.Add(weapon.DefensiveModifier);
            
            if(_unitEquipmentData.TryGetEquippedArmor(out var armor) && armor.DefensiveModifier)
                defensiveModifiers.Add(armor.DefensiveModifier);
        }
    }
}