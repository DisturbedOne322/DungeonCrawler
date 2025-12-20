using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.ShopRooms.Shop;
using PopupControllers;
using StateMachine.Shop;
using UI.Menus;
using UnityEngine;
using Zenject;

namespace Installers.ServiceInstallers
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private ShopRoom _shopRoom;

        public override void InstallBindings()
        {
            Container.Bind<ShopRoom>().FromInstance(_shopRoom).AsSingle();

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