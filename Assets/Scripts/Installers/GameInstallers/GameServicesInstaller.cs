using Gameplay.Enemies;
using Gameplay.Player;
using Gameplay.Rewards;
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
            Container.Bind<EnemyFactory>().AsSingle();
            
            Container.Bind<NumberObjectsPool>().AsTransient();
            Container.Bind<NumberObjectsAnimator>().AsSingle();
            
            Container.Bind<BalanceService>().AsSingle();
            
            Container.Bind<RewardDistributor>().AsSingle();
            Container.Bind<RewardSelectorService>().AsSingle();
            Container.Bind<RoomDropsService>().AsSingle();
        }
    }
}