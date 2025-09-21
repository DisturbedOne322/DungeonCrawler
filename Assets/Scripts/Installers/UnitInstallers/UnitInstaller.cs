using Gameplay.Combat;
using Gameplay.Combat.Data;
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
            Container.Bind<UnitStatsData>().AsSingle();
        }
    }
}