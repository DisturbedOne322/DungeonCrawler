using Animations;
using Gameplay.Combat;
using Gameplay.Combat.SkillSelection;
using Gameplay.Equipment;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.Visual;
using UnityEngine;
using Zenject;

namespace Gameplay.Units
{
    public class GameUnit : MonoBehaviour, IGameUnit
    {
        [SerializeField] private EvadeAnimator _evadeAnimator;
        [SerializeField] private AttackAnimator _attackAnimator;

        private StatusEffectsProcessor _statusEffectsProcessor;

        public EvadeAnimator EvadeAnimator => _evadeAnimator;
        public AttackAnimator AttackAnimator => _attackAnimator;

        public string EntityName { get; private set; }
        public Vector3 Position => transform.position;

        [Inject]
        private void Construct(
            StatusEffectsProcessor statusEffectsProcessor,
            UnitHealthData unitHealthData,
            UnitHealthController unitHealthController,
            UnitManaData unitManaData,
            UnitManaController unitManaController,
            BaseActionSelectionProvider baseActionSelectionProvider,
            UnitStatsData unitStatsData,
            UnitBonusStatsData unitBonusStatsData,
            UnitSkillsContainer unitSkillsContainer,
            UnitHeldStatusEffectsContainer unitHeldStatusEffectsContainer,
            UnitActiveStatusEffectsContainer unitActiveStatusEffectsContainer,
            UnitInventoryData unitInventoryData,
            UnitEquipmentData unitEquipmentData)
        {
            _statusEffectsProcessor = statusEffectsProcessor;

            UnitHealthData = unitHealthData;
            UnitHealthController = unitHealthController;
            UnitManaData = unitManaData;
            UnitManaController = unitManaController;
            BaseActionSelectionProvider = baseActionSelectionProvider;
            UnitStatsData = unitStatsData;
            UnitBonusStatsData = unitBonusStatsData;
            UnitSkillsContainer = unitSkillsContainer;
            UnitHeldStatusEffectsContainer = unitHeldStatusEffectsContainer;
            UnitActiveStatusEffectsContainer = unitActiveStatusEffectsContainer;
            UnitInventoryData = unitInventoryData;
            UnitEquipmentData = unitEquipmentData;
        }

        public virtual void InitializeUnit(UnitData unitData)
        {
            EntityName = unitData.Name;

            UnitHealthData.Initialize(unitData.MaxHp);
            UnitManaData.Initialize(unitData.MaxMp);

            UnitStatsData.SetStats(unitData.UnitStartingStats);
            UnitBonusStatsData.SetData(unitData.UnitStartingBonusStats);

            UnitSkillsContainer.AssignStartingSkills(unitData);
            UnitInventoryData.AddItems(unitData.Items);

            foreach (var statusEffect in unitData.StatusEffects)
                _statusEffectsProcessor.AddStatusEffectTo(this, statusEffect, statusEffect);

            if (unitData.BaseWeapon)
                UnitEquipmentData.EquipWeapon(unitData.BaseWeapon);

            if (unitData.BaseArmor)
                UnitEquipmentData.EquipArmor(unitData.BaseArmor);
        }

        #region Unit Data

        public UnitHealthData UnitHealthData { get; private set; }
        public UnitManaData UnitManaData { get; private set; }
        public UnitStatsData UnitStatsData { get; private set; }
        public UnitBonusStatsData UnitBonusStatsData { get; private set; }
        public UnitSkillsContainer UnitSkillsContainer { get; private set; }
        public UnitHeldStatusEffectsContainer UnitHeldStatusEffectsContainer { get; private set; }
        public UnitActiveStatusEffectsContainer UnitActiveStatusEffectsContainer { get; private set; }
        public UnitInventoryData UnitInventoryData { get; private set; }
        public UnitEquipmentData UnitEquipmentData { get; private set; }

        #endregion

        #region Unit Controllers

        public UnitHealthController UnitHealthController { get; private set; }
        public UnitManaController UnitManaController { get; private set; }
        public BaseActionSelectionProvider BaseActionSelectionProvider { get; private set; }

        #endregion
    }
}