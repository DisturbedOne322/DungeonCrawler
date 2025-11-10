using Gameplay.Dungeon.Rooms.BaseSellableItems;
using PopupControllers;
using UI.BattleMenu;
using Zenject;

namespace Installers.ServiceInstallers
{
    public abstract class SinglePurchaseRoomInstaller : MonoInstaller
    {
        protected abstract BaseSellableItemsConfig ItemsConfig { get; }

        public override void InstallBindings()
        {
            Container.Bind<BaseSellableItemsConfig>().FromInstance(ItemsConfig).AsSingle();

            Container.Bind<SingleTypePurchaseController>().AsSingle();
            Container.Bind<SellableItemsProvider>().AsSingle();
            Container.Bind<ItemSellingController>().AsSingle();
            Container.Bind<MenuItemsUpdater>().AsSingle();
        }
    }
}