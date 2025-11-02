using System;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using UI;
using UI.Gameplay;
using UniRx;

namespace StateMachine.Shop
{
    public class ShopController
    {
        private readonly ShopStateMachine _stateMachine;
        private readonly UIFactory _uiFactory;
        private readonly PlayerInputProvider _playerInputProvider;

        private IDisposable _disposable;
        
        public ShopController(ShopStateMachine stateMachine, UIFactory uiFactory, PlayerInputProvider playerInputProvider)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _playerInputProvider = playerInputProvider;
        }

        public async UniTask RunShop()
        {
            UniTaskCompletionSource cts = new();

            var popup = _uiFactory.CreatePopup<ShopPopup>();
            popup.Initialize(_stateMachine);
            
            _playerInputProvider.AddUiInputOwner();
            _stateMachine.OpenShopMenu();
            
            _disposable = _stateMachine.OnShopExit.Subscribe(_ =>
            {
                cts.TrySetResult();
                _disposable.Dispose();
            });
            
            await cts.Task;
            _playerInputProvider.RemoveUiInputOwner();

            await popup.HidePopup();
        }
    }
}