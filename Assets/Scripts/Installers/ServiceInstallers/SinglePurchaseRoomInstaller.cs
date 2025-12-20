using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using PopupControllers;
using UI.Menus;
using UnityEngine;
using Zenject;

namespace Installers.ServiceInstallers
{
    public class SinglePurchaseRoomInstaller : MonoInstaller
    {
        [SerializeField] private SinglePurchaseRoom _singlePurchaseRoom;
        
        public override void InstallBindings()
        {
            Container.Bind<SinglePurchaseRoom>().FromInstance(_singlePurchaseRoom).AsSingle();

            Container.Bind<SingleTypePurchaseController>().AsSingle();
            Container.Bind<PurchasableItemsProvider>().AsSingle();
            Container.Bind<ItemPurchaseController>().AsSingle();
            Container.Bind<MenuItemsUpdater>().AsSingle();
        }
    }
}