using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Player;
using Gameplay.Units;
using StateMachine.Core;
using UI.BattleMenu;
using UniRx;

namespace StateMachine.BattleMenu
{
    public abstract class BattleMenuState : BaseState<BattleMenuState, BattleMenuStateMachine>
    {
        protected readonly CombatService CombatService;

        public readonly MenuItemsUpdater MenuItemsUpdater;
        protected readonly PlayerUnit Player;
        protected readonly PlayerInputProvider PlayerInputProvider;

        protected CompositeDisposable Disposables;

        protected List<MenuItemData> MenuItems = new();

        public BattleMenuState(PlayerUnit player,
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater,
            CombatService combatService)
        {
            Player = player;
            PlayerInputProvider = playerInputProvider;
            MenuItemsUpdater = menuItemsUpdater;
            CombatService = combatService;
        }

        public override UniTask EnterState()
        {
            SubscribeToInputEvents();
            MenuItemsUpdater.ResetSelection();
            return UniTask.CompletedTask;
        }

        public override UniTask ExitState()
        {
            UnsubscribeFromInputEvents();
            return UniTask.CompletedTask;
        }

        public abstract void LoadMenuItems();

        protected abstract void SubscribeToInputEvents();

        private void UnsubscribeFromInputEvents()
        {
            Disposables?.Dispose();
        }
    }
}