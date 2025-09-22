using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Player;
using Gameplay.Units;
using States;
using UnityEngine;

namespace StateMachine.BattleMenu
{
    public abstract class BattleMenuState : BaseState<BattleMenuState, BattleMenuStateMachine>
    {
        protected readonly PlayerUnit Player;
        protected readonly PlayerInputProvider PlayerInputProvider;
        protected readonly CombatService CombatService;
        
        public readonly MenuItemsUpdater MenuItemsUpdater;

        protected List<BattleMenuItemData> MenuItems = new();
        
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
            MenuItemsUpdater.ResetSelection();
            LoadMenuItems();
            SubscribeToInputEvents();
            return UniTask.CompletedTask;
        }

        public override UniTask ExitState()
        {
            UnsubscribeFromInputEvents();
            return UniTask.CompletedTask;
        }

        protected abstract void LoadMenuItems();
        protected abstract void SubscribeToInputEvents();
        protected abstract void UnsubscribeFromInputEvents();
    }
}