using Gameplay.Enemies;
using Gameplay.Rewards;
using Gameplay.Services;
using PopupControllers;
using PopupControllers.SkillDiscarding;
using UI.BattleUI.Damage;
using Zenject;

namespace Installers.GameInstallers
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

            Container.Bind<ItemsDistributor>().AsSingle();
            Container.Bind<PurchaseDistributionStrategy>().AsSingle();
            Container.Bind<LootDistributionStrategy>().AsSingle();
            Container.Bind<RewardSelectorService>().AsSingle();
            Container.Bind<RoomDropsService>().AsSingle();
            
            Container.Bind<EquipmentChangeController>().AsSingle();
            
            Container.Bind<SkillDiscardController>().AsSingle();
            Container.Bind<LootSkillDiscardStrategy>().AsSingle();
            Container.Bind<PurchasedSkillDiscardStrategy>().AsSingle();
        }
    }
}