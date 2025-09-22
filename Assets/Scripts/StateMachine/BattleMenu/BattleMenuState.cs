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
        protected readonly MenuItemsUpdater MenuItemsUpdater;
        protected readonly CombatService CombatService;

        protected List<BattleMenuItem> MenuItems = new();
        
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