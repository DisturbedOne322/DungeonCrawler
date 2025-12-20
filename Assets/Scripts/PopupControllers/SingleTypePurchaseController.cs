using System;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using UI;
using UI.BattleMenu;
using UI.Gameplay;
using UI.Menus;
using UI.Popups;

namespace PopupControllers
{
    public class SingleTypePurchaseController
    {
        private readonly ItemPurchaseController _itemPurchaseController;
        private readonly MenuItemsUpdater _menuItemsUpdater;
        private readonly UIFactory _uiFactory;
        private IDisposable _disposable;

        public SingleTypePurchaseController(UIFactory uiFactory,
            ItemPurchaseController itemPurchaseController, MenuItemsUpdater menuItemsUpdater)
        {
            _uiFactory = uiFactory;
            _itemPurchaseController = itemPurchaseController;
            _menuItemsUpdater = menuItemsUpdater;
        }

        public async UniTask RunShop()
        {
            _itemPurchaseController.Initialize();

            var popup = _uiFactory.CreatePopup<SingleTypePurchasePopup>();
            popup.Initialize(_menuItemsUpdater, "SHRINE");

            await _itemPurchaseController.ProcessSelling();

            await popup.HidePopup();
        }
    }
}