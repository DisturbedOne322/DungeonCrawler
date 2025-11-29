using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Services;
using Gameplay.Player;
using Gameplay.Units;
using StateMachine.Core;
using UI.BattleMenu;
using UI.Menus;
using UI.Menus.Data;

namespace StateMachine.BattleMenu
{
    public abstract class BattleMenuState : BaseState<BattleMenuState, BattleMenuStateMachine>, IUiInputHandler
    {
        private readonly PlayerInputProvider _playerInputProvider;
        protected readonly CombatService CombatService;
        protected readonly List<MenuItemData> MenuItems = new();

        public readonly MenuItemsUpdater MenuItemsUpdater;
        protected readonly PlayerUnit Player;

        public BattleMenuState(PlayerUnit player,
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater,
            CombatService combatService)
        {
            Player = player;
            _playerInputProvider = playerInputProvider;
            MenuItemsUpdater = menuItemsUpdater;
            CombatService = combatService;
        }

        public virtual void OnUISubmit()
        {
        }

        public virtual void OnUIBack()
        {
        }

        public virtual void OnUIUp()
        {
        }

        public virtual void OnUIDown()
        {
        }

        public virtual void OnUILeft()
        {
        }

        public virtual void OnUIRight()
        {
        }

        public override UniTask EnterState()
        {
            _playerInputProvider.AddUiInputOwner(this);
            MenuItemsUpdater.ResetSelection();
            return UniTask.CompletedTask;
        }

        public override UniTask ExitState()
        {
            _playerInputProvider.RemoveUiInputOwner(this);
            return UniTask.CompletedTask;
        }

        public void LoadMenuItems()
        {
            MenuItems.Clear();
            InitMenuItems();
            MenuItemsUpdater.SetMenuItems(MenuItems);
        }

        public abstract void InitMenuItems();
    }
}