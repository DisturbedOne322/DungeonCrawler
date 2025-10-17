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

        public Vector3 Position => transform.position;

        public UnitHealthData UnitHealthData { get; private set; }

        public UnitManaData UnitManaData { get; private set; }

        public UnitHealthController UnitHealthController { get; private set; }

        public UnitManaController UnitManaController { get; private set; }

        public ActionSelectionProvider ActionSelectionProvider { get; private set; }

        public UnitStatsData UnitStatsData { get; private set; }

        public UnitBonusStatsData UnitBonusStatsData { get; private set; }

        public UnitSkillsData UnitSkillsData { get; private set; }

        public UnitBuffsData UnitBuffsData { get; private set; }
        public UnitDebuffsData UnitDebuffsData { get; private set; }

        public UnitActiveBuffsData UnitActiveBuffsData { get; private set; }

        public UnitActiveDebuffsData UnitActiveDebuffsData { get; private set; }

        public UnitInventoryData UnitInventoryData { get; private set; }

        public UnitEquipmentData UnitEquipmentData { get; private set; }

        public EvadeAnimator EvadeAnimator => _evadeAnimator;
        public AttackAnimator AttackAnimator => _attackAnimator;

        public string EntityName { get; private set; }

        [Inject]
        private void Construct(UnitHealthData unitHealthData,
            UnitHealthController unitHealthController,
            UnitManaData unitManaData,
            UnitManaController unitManaController,
            ActionSelectionProvider actionSelectionProvider,
            UnitStatsData unitStatsData,
            UnitBonusStatsData unitBonusStatsData,
            UnitSkillsData unitSkillsData,
            UnitBuffsData unitBuffsData,
            UnitDebuffsData unitDebuffsesData,
            UnitActiveBuffsData unitActiveBuffsData,
            UnitActiveDebuffsData unitActiveDebuffsData,
            UnitInventoryData unitInventoryData,
            UnitEquipmentData unitEquipmentData)
        {
            UnitHealthData = unitHealthData;
            UnitHealthController = unitHealthController;
            UnitManaData = unitManaData;
            UnitManaController = unitManaController;
            ActionSelectionProvider = actionSelectionProvider;
            UnitStatsData = unitStatsData;
            UnitBonusStatsData = unitBonusStatsData;
            UnitSkillsData = unitSkillsData;
            UnitBuffsData = unitBuffsData;
            UnitDebuffsData = unitDebuffsesData;
            UnitActiveBuffsData = unitActiveBuffsData;
            UnitActiveDebuffsData = unitActiveDebuffsData;
            UnitInventoryData = unitInventoryData;
            UnitEquipmentData = unitEquipmentData;
        }

        public void InitializeUnit(UnitData unitData)
        {
            EntityName = unitData.Name;

            UnitHealthData.Initialize(unitData.MaxHp);
            UnitManaData.Initialize(unitData.MaxMp);

            UnitStatsData.SetStats(unitData.UnitStartingStats);

            UnitSkillsData.AssignSkills(unitData);
            UnitInventoryData.AddItems(unitData.Items);

            if (unitData.BaseWeapon)
                UnitEquipmentData.EquipWeapon(unitData.BaseWeapon);

            if (unitData.BaseArmor)
                UnitEquipmentData.EquipArmor(unitData.BaseArmor);
        }
    }
}