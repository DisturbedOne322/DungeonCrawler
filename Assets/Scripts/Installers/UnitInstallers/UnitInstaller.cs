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
            Container.BindInstance(_gameUnit).AsSingle();

            Container.Bind<UnitHealthData>().AsSingle();
            Container.Bind<UnitHealthController>().AsSingle();

            Container.Bind<UnitManaData>().AsSingle();
            Container.Bind<UnitManaController>().AsSingle();

            Container.Bind<UnitSkillsData>().AsSingle();
            Container.Bind<UnitInventoryData>().AsSingle();
            Container.Bind<UnitStatsData>().AsSingle();
            Container.Bind<UnitBonusStatsData>().AsSingle();
            Container.Bind<UnitHeldStatusEffectsData>().AsSingle();
            Container.Bind<UnitActiveStatusEffectsData>().AsSingle();
            Container.Bind<UnitEquipmentData>().AsSingle();

            Container.Bind<OnObtainStatusEffectApplier>().AsSingle().NonLazy();
            Container.Bind<EquipmentStatBuffsApplier>().AsSingle().NonLazy();
        }
    }
}