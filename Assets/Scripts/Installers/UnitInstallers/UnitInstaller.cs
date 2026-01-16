using Gameplay.Combat;
using Gameplay.Equipment;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.Units;
using UnityEngine;
using Zenject;

namespace Installers.UnitInstallers
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private GameUnit _gameUnit;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameUnit>().FromInstance(_gameUnit).AsSingle();

            Container.Bind<UnitHealthData>().AsSingle();
            Container.Bind<UnitHealthController>().AsSingle();

            Container.Bind<UnitManaData>().AsSingle();
            Container.Bind<UnitManaController>().AsSingle();

            Container.Bind<UnitSkillsContainer>().AsSingle();
            Container.Bind<UnitInventoryData>().AsSingle();
            Container.Bind<UnitStatsData>().AsSingle();
            Container.Bind<UnitBonusStatsData>().AsSingle();
            Container.Bind<UnitHeldStatusEffectsContainer>().AsSingle();
            Container.Bind<UnitActiveStatusEffectsContainer>().AsSingle();
            Container.Bind<UnitEquipmentData>().AsSingle();

            Container.Bind<EquipmentStatusEffectApplier>().AsSingle().NonLazy();
            Container.Bind<EquipmentStatBuffsApplier>().AsSingle().NonLazy();
        }
    }
}