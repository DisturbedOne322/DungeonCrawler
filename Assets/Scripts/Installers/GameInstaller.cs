using Data;
using Gameplay;
using Gameplay.Player;
using Gameplay.Services;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapper>().AsSingle();
            Container.Bind<GameSequenceController>().AsSingle();
            Container.Bind<GameplayData>().AsSingle();
            Container.Bind<BalanceService>().AsSingle();
        }
    }
}