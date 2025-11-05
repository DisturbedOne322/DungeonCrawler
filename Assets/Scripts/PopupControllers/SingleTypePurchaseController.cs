using System;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Player;
using UI;
using UI.BattleMenu;
using UI.Gameplay;

namespace PopupControllers
{
    public class SingleTypePurchaseController
    {
        private readonly UIFactory _uiFactory;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly ItemSellingController _itemSellingController;
        private readonly MenuItemsUpdater _menuItemsUpdater;
        private IDisposable _disposable;
        
        public SingleTypePurchaseController(UIFactory uiFactory, PlayerInputProvider playerInputProvider,
            ItemSellingController itemSellingController, MenuItemsUpdater menuItemsUpdater)
        {
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
            _itemSellingController = itemSellingController;
            _menuItemsUpdater = menuItemsUpdater;
        }

        public async UniTask RunShop()
        {
            _itemSellingController.Initialize();
            
            var popup = _uiFactory.CreatePopup<SingleTypePurchasePopup>();
            popup.Initialize(_menuItemsUpdater, "SHRINE");
            
            await _playerInputProvider.EnableUIInputUntil(_itemSellingController.ProcessSelling());

            await popup.HidePopup();
        }
    }
}