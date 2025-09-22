using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Skills;
using Gameplay.Player;
using States;
using UniRx;
using UnityEngine;

namespace StateMachine.BattleMenu
{
    public class BattleMenuStateMachine 
        : StateMachine<BattleMenuState, BattleMenuStateMachine>
    {
        private readonly PlayerInputProvider _playerInputProvider;
        
        public readonly Subject<BaseSkill> SkillSelected = new();

        public BattleMenuStateMachine(IEnumerable<BattleMenuState> states, PlayerInputProvider playerInputProvider) :
            base(states)
        {
            _playerInputProvider = playerInputProvider;
        }

        public void OpenBattleMenu()
        {
            _playerInputProvider.EnableUiInput(true);
            GoToState<MainBattleMenuState>().Forget();
        }

        public void SelectSkill(BaseSkill skill)
        {
            _playerInputProvider.EnableUiInput(false);
            SkillSelected.OnNext(skill);
            Reset();
        }
    }
}