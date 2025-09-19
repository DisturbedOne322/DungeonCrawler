using Gameplay.Dungeon;
using Gameplay.Services;
using Zenject;

namespace Installers
{
    public class DungeonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DungeonGenerator>().AsSingle();
            Container.Bind<DungeonBranchingController>().AsSingle();
            Container.Bind<DungeonPositioner>().AsSingle();
            Container.Bind<DungeonLayoutProvider>().AsSingle();
            Container.Bind<DungeonFactory>().FromComponentInHierarchy().AsSingle();
        }
    }
}