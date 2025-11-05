using System;
using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Player;
using UI;
using UI.Gameplay;

namespace PopupControllers
{
    public class SingleTypePurchaseController
    {
        private readonly UIFactory _uiFactory;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly ItemSellingController _itemSellingController;

        private IDisposable _disposable;
        
        public SingleTypePurchaseController(UIFactory uiFactory, PlayerInputProvider playerInputProvider,
            ItemSellingController itemSellingController)
        {
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
            _itemSellingController = itemSellingController;
        }

        public async UniTask RunShop()
        {
            //var popup = _uiFactory.CreatePopup<ShopPopup>();
            
            await _playerInputProvider.EnableUIInputUntil(_itemSellingController.ProcessSelling());

            //await popup.HidePopup();
        }
    }
}