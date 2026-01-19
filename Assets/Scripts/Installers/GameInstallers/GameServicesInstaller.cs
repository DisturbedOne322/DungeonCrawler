using Gameplay;
using Gameplay.Enemies;
using Gameplay.Pause;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Traps;
using PopupControllers;
using PopupControllers.Pause;
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

            Container.BindInterfacesAndSelfTo<PauseController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayContext>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeScaleController>()
                .AsSingle();
            Container.Bind<PausePopupController>().AsSingle();

            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<EnemyFactory>().AsSingle();
            Container.Bind<TrapFactory>().AsSingle();
        }
    }
}