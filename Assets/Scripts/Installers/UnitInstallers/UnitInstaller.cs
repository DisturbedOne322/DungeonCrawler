using Gameplay.Buffs;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Zenject;

namespace Installers.UnitInstallers
{
    public class UnitInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UnitHealthData>().AsSingle();
            Container.Bind<UnitHealthController>().AsSingle();
            
            Container.Bind<UnitSkillsData>().AsSingle();
            Container.Bind<UnitInventoryData>().AsSingle();
            Container.Bind<UnitStatsData>().AsSingle();
            Container.Bind<UnitBuffsData>().AsSingle();
            Container.Bind<WeaponBuffApplier>().AsSingle().NonLazy();
            Container.Bind<UnitActiveBuffsData>().AsSingle();
            Container.Bind<UnitEquipmentData>().AsSingle();
        }
    }
}