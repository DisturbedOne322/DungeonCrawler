using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using StateMachine.Core;
using UniRx;

namespace StateMachine.Shop
{
    public class ShopStateMachine : StateMachine<BaseShopState, ShopStateMachine>
    {
        private readonly PlayerInputProvider _playerInputProvider;
        
        public Subject<Unit> OnShopExit = new();

        public ShopStateMachine(IEnumerable<BaseShopState> states, 
            PlayerInputProvider playerInputProvider) : base(states)
        {
            _playerInputProvider = playerInputProvider;
        }

        public void OpenShopMenu()
        {
            LoadMenus();
            _playerInputProvider.EnableUiInput(true);
            GoToState<MainShopState>().Forget();
        }

        public void InvokeShopExit()
        {
            OnShopExit?.OnNext(Unit.Default);
        }

        private void LoadMenus()
        {
            foreach (var typeStateKv in States)
                typeStateKv.Value.LoadMenuItems();
        }
    }
}