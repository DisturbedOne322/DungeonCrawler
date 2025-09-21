using Gameplay.Enemies;
using Gameplay.Player;
using Gameplay.Services;
using UI;
using UI.BattleUI.Damage;
using Zenject;

namespace Installers.SceneInstallers
{
    public class GameServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ContainerFactory>().AsSingle();
            Container.Bind<EnemyFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIFactory>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<DamageNumbersPool>().AsTransient();
            Container.Bind<DamageNumbersAnimator>().AsSingle();
            
            Container.Bind<BalanceService>().AsSingle();
        }
    }
}