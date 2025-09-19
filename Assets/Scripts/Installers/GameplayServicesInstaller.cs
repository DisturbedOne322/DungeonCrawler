using Gameplay.Services;
using Zenject;

namespace Installers
{
    public class GameplayServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ContainerFactory>().AsSingle();
        }
    }
}