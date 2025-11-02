using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace StateMachine.Shop
{
    public class ShopController
    {
        private readonly ShopStateMachine _stateMachine;

        private IDisposable _disposable;
        
        public ShopController(ShopStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async UniTask RunShop()
        {
            UniTaskCompletionSource cts = new();
            
            _stateMachine.OpenShopMenu();
            
            _disposable = _stateMachine.OnShopExit.Subscribe(_ =>
            {
                cts.TrySetResult();
                _disposable.Dispose();
            });
            
            await cts.Task;
        }
    }
}