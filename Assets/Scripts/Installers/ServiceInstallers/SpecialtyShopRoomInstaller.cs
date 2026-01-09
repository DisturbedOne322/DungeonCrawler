using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using PopupControllers;
using UI.Menus;
using UnityEngine;
using Zenject;

namespace Installers.ServiceInstallers
{
    [DisallowMultipleComponent]
    public class SpecialtyShopRoomInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SpecialtyShopRoom>().FromInstance(GetComponent<SpecialtyShopRoom>()).AsSingle();

            Container.Bind<SingleTypePurchaseController>().AsSingle();
            Container.Bind<PurchasableItemsProvider>().AsSingle();
            Container.Bind<ItemPurchaseController>().AsSingle();
            Container.Bind<MenuItemsUpdater>().AsSingle();
        }
    }
}