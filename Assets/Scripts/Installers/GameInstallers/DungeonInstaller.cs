using Gameplay.Dungeon;
using Gameplay.Services;
using Zenject;

namespace Installers.SceneInstallers
{
    public class DungeonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DungeonGenerator>().AsSingle();
            Container.Bind<DungeonBranchingController>().AsSingle();
            Container.Bind<DungeonBranchingSelector>().AsSingle();
            Container.Bind<DungeonPositioner>().AsSingle();
            Container.Bind<DungeonLayoutProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<DungeonRoomsPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<DungeonFactory>().AsSingle();
        }
    }
}