using System;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using StateMachine.Shop;
using UI;
using UI.Popups;
using UniRx;

namespace PopupControllers
{
    public class ShopController
    {
        private readonly ShopStateMachine _stateMachine;
        private readonly UIFactory _uiFactory;

        private IDisposable _disposable;

        public ShopController(ShopStateMachine stateMachine, UIFactory uiFactory,
            PlayerInputProvider playerInputProvider)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
        }

        public async UniTask RunShop()
        {
            UniTaskCompletionSource cts = new();

            var popup = _uiFactory.CreatePopup<ShopPopup>();
            popup.Initialize(_stateMachine);

            _stateMachine.OpenShopMenu();

            _disposable = _stateMachine.OnShopExit.Subscribe(_ =>
            {
                cts.TrySetResult();
                _disposable.Dispose();
            });

            await cts.Task;

            await popup.HidePopup();
        }
    }
}