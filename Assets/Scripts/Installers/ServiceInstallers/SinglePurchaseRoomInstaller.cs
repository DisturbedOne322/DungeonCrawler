using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using PopupControllers;
using UI.BattleMenu;
using Zenject;

namespace Installers.ServiceInstallers
{
    public abstract class SinglePurchaseRoomInstaller : MonoInstaller
    {
        protected abstract BasePurchasableItemsConfig ItemsConfig { get; }

        public override void InstallBindings()
        {
            Container.Bind<BasePurchasableItemsConfig>().FromInstance(ItemsConfig).AsSingle();

            Container.Bind<SingleTypePurchaseController>().AsSingle();
            Container.Bind<PurchasableItemsProvider>().AsSingle();
            Container.Bind<ItemPurchaseController>().AsSingle();
            Container.Bind<MenuItemsUpdater>().AsSingle();
        }
    }
}