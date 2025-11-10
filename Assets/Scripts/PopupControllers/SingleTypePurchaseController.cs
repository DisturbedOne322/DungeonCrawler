using System;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using UI;
using UI.BattleMenu;
using UI.Gameplay;

namespace PopupControllers
{
    public class SingleTypePurchaseController
    {
        private readonly ItemSellingController _itemSellingController;
        private readonly MenuItemsUpdater _menuItemsUpdater;
        private readonly UIFactory _uiFactory;
        private IDisposable _disposable;

        public SingleTypePurchaseController(UIFactory uiFactory,
            ItemSellingController itemSellingController, MenuItemsUpdater menuItemsUpdater)
        {
            _uiFactory = uiFactory;
            _itemSellingController = itemSellingController;
            _menuItemsUpdater = menuItemsUpdater;
        }

        public async UniTask RunShop()
        {
            _itemSellingController.Initialize();

            var popup = _uiFactory.CreatePopup<SingleTypePurchasePopup>();
            popup.Initialize(_menuItemsUpdater, "SHRINE");

            await _itemSellingController.ProcessSelling();

            await popup.HidePopup();
        }
    }
}