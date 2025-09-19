using Data;
using Gameplay.Player;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerMovementController>().AsSingle();
            Container.Bind<PlayerMovementHistory>().AsSingle();
            Container.Bind<PlayerDecisionProvider>().AsSingle();
            Container.Bind<PlayerInputProvider>().AsSingle();
        }
    }
}