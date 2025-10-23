using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.StatusEffects.Buffs.Services;
using Zenject;

namespace Installers.UnitInstallers
{
    public class UnitInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UnitHealthData>().AsSingle();
            Container.Bind<UnitHealthController>().AsSingle();

            Container.Bind<UnitManaData>().AsSingle();
            Container.Bind<UnitManaController>().AsSingle();

            Container.Bind<UnitSkillsData>().AsSingle();
            Container.Bind<UnitInventoryData>().AsSingle();
            Container.Bind<UnitStatsData>().AsSingle();
            Container.Bind<UnitBonusStatsData>().AsSingle();
            Container.Bind<UnitHeldStatusEffectsData>().AsSingle();
            Container.Bind<EquipmentStatusEffectApplier>().AsSingle().NonLazy();
            Container.Bind<UnitActiveStatusEffectsData>().AsSingle();
            Container.Bind<UnitEquipmentData>().AsSingle();
        }
    }
}