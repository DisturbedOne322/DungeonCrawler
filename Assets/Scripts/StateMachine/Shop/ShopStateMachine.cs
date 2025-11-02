using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using StateMachine.Core;
using UniRx;
using UnityEngine;

namespace StateMachine.Shop
{
    public class ShopStateMachine : StateMachine<BaseShopState, ShopStateMachine>
    {
        public Subject<Unit> OnShopExit = new();

        public ShopStateMachine(IEnumerable<BaseShopState> states) : base(states)
        {
            
        }

        public void OpenShopMenu()
        {
            LoadMenus();
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