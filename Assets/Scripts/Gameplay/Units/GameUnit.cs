using Animations;
using Gameplay.Combat;
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


        #region Unit Data
        
        public UnitHealthData UnitHealthData { get; private set; }
        public UnitManaData UnitManaData { get; private set; }
        public UnitStatsData UnitStatsData { get; private set; }
        public UnitBonusStatsData UnitBonusStatsData { get; private set; }
        public UnitSkillsData UnitSkillsData { get; private set; }
        public UnitHeldStatusEffectsData UnitHeldStatusEffectsData { get; private set; }
        public UnitActiveStatusEffectsData UnitActiveStatusEffectsData { get; private set; }
        public UnitInventoryData UnitInventoryData { get; private set; }
        public UnitEquipmentData UnitEquipmentData { get; private set; }
        
        #endregion

        #region Unit Controllers

        public UnitHealthController UnitHealthController { get; private set; }
        public UnitManaController UnitManaController { get; private set; }
        public BaseActionSelectionProvider BaseActionSelectionProvider { get; private set; }

        #endregion

        public EvadeAnimator EvadeAnimator => _evadeAnimator;
        public AttackAnimator AttackAnimator => _attackAnimator;

        public string EntityName { get; private set; }
        public Vector3 Position => transform.position;

        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController,
            UnitManaData unitManaData,
            UnitManaController unitManaController,
            BaseActionSelectionProvider baseActionSelectionProvider,
            UnitStatsData unitStatsData,
            UnitBonusStatsData unitBonusStatsData,
            UnitSkillsData unitSkillsData,
            UnitHeldStatusEffectsData unitHeldStatusEffectsData,
            UnitActiveStatusEffectsData unitActiveStatusEffectsData,
            UnitInventoryData unitInventoryData,
            UnitEquipmentData unitEquipmentData)
        {
            UnitHealthData = unitHealthData;
            UnitHealthController = unitHealthController;
            UnitManaData = unitManaData;
            UnitManaController = unitManaController;
            BaseActionSelectionProvider = baseActionSelectionProvider;
            UnitStatsData = unitStatsData;
            UnitBonusStatsData = unitBonusStatsData;
            UnitSkillsData = unitSkillsData;
            UnitHeldStatusEffectsData = unitHeldStatusEffectsData;
            UnitActiveStatusEffectsData = unitActiveStatusEffectsData;
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

            UnitSkillsData.AssignStartingSkills(unitData);
            UnitInventoryData.AddItems(unitData.Items);
            
            UnitHeldStatusEffectsData.AssignStartingStatusEffects(unitData.StatusEffects);

            if (unitData.BaseWeapon)
                UnitEquipmentData.EquipWeapon(unitData.BaseWeapon);

            if (unitData.BaseArmor)
                UnitEquipmentData.EquipArmor(unitData.BaseArmor);
        }
    }
}