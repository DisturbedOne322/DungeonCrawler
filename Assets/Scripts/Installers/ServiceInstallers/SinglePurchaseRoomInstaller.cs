using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Dungeon.RoomTypes;
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