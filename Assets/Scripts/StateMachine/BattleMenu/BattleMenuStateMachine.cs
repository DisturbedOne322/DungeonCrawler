using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Skills;
using Gameplay.Player;
using States;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class BattleMenuStateMachine 
        : StateMachine<BattleMenuState, BattleMenuStateMachine>
    {
        private readonly PlayerInputProvider _playerInputProvider;
        
        public readonly Subject<BaseSkill> SkillSelected = new();
        public readonly Subject<Unit> OnOpened = new();

        public BattleMenuStateMachine(IEnumerable<BattleMenuState> states, PlayerInputProvider playerInputProvider) :
            base(states)
        {
            _playerInputProvider = playerInputProvider;
        }

        public void OpenBattleMenu()
        {
            ResetMenus();
            
            _playerInputProvider.EnableUiInput(true);
            GoToState<MainBattleMenuState>().Forget();
            
            OnOpened.OnNext(Unit.Default);
        }

        private void ResetMenus()
        {
            foreach (var typeStateKv in States)
            {
                typeStateKv.Value.MenuItemsUpdater.ResetSelection(false);
                typeStateKv.Value.LoadMenuItems();
            }
        }

        public void SelectSkill(BaseSkill skill)
        {
            _playerInputProvider.EnableUiInput(false);
            SkillSelected.OnNext(skill);
            Reset();
        }
    }
}