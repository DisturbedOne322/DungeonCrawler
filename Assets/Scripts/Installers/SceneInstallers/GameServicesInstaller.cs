using Gameplay;
using Gameplay.Player;
using Gameplay.Services;
using UI;
using Zenject;

namespace Installers
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ContainerFactory>().AsSingle();
            Container.Bind<EnemyFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIFactory>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<BalanceService>().AsSingle();
        }
    }
}