using Gameplay.Shop;
using StateMachine.Shop;
using UI.BattleMenu;
using UnityEngine;
using Zenject;

namespace Installers.ServiceInstallers
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private ShopItemsConfig _shopItemsConfig;

        public override void InstallBindings()
        {
            Container.Bind<ShopItemsConfig>().FromInstance(_shopItemsConfig).AsSingle();

            Container.Bind<ShopController>().AsSingle();
            Container.Bind<ShopItemsProvider>().AsSingle();
            Container.Bind<MenuItemsUpdater>().AsTransient();

            Container.Bind<ShopStateMachine>().AsSingle();
            Container.Bind<BaseShopState>().To<MainShopState>().AsSingle();
            Container.Bind<BaseShopState>().To<ConsumablesShopState>().AsSingle();
            Container.Bind<BaseShopState>().To<EquipmentShopState>().AsSingle();
            Container.Bind<BaseShopState>().To<SkillsShopState>().AsSingle();
            Container.Bind<BaseShopState>().To<StatusEffectsShopState>().AsSingle();
        }
    }
}