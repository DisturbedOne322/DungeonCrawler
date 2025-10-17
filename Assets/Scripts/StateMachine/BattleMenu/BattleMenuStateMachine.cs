using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Combat.Services;
using Gameplay.Player;
using StateMachine.Core;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class BattleMenuStateMachine
        : StateMachine<BattleMenuState, BattleMenuStateMachine>
    {
        private readonly PlayerInputProvider _playerInputProvider;

        public readonly Subject<BaseCombatAction> ActionSelected = new();

        public BattleMenuStateMachine(IEnumerable<BattleMenuState> states,
            PlayerInputProvider playerInputProvider,
            CombatEventsBus combatEventsBus) :
            base(states)
        {
            _playerInputProvider = playerInputProvider;
            combatEventsBus.OnCombatEnded.Subscribe(_ => ResetMenus());
        }

        public void OpenBattleMenu()
        {
            LoadMenus();
            _playerInputProvider.EnableUiInput(true);
            GoToState<MainBattleMenuState>().Forget();
        }

        public void SelectAction(BaseCombatAction action)
        {
            _playerInputProvider.EnableUiInput(false);
            ActionSelected.OnNext(action);
            ResetStateMachine();
        }

        private void LoadMenus()
        {
            foreach (var typeStateKv in States)
                typeStateKv.Value.LoadMenuItems();
        }

        private void ResetMenus()
        {
            foreach (var typeStateKv in States)
                typeStateKv.Value.MenuItemsUpdater.ResetSelection(false);
        }
    }
}