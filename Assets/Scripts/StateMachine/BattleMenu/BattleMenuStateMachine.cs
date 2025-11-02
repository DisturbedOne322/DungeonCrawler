using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Combat.Services;
using StateMachine.Core;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class BattleMenuStateMachine : StateMachine<BattleMenuState, BattleMenuStateMachine>
    {

        public readonly Subject<BaseCombatAction> ActionSelected = new();

        public BattleMenuStateMachine(IEnumerable<BattleMenuState> states,
            CombatEventsBus combatEventsBus) :
            base(states)
        {
            combatEventsBus.OnCombatEnded.Subscribe(_ => ResetMenus());
        }

        public void OpenBattleMenu()
        {
            LoadMenus();
            GoToState<MainBattleMenuState>().Forget();
        }

        public void SelectAction(BaseCombatAction action)
        {
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