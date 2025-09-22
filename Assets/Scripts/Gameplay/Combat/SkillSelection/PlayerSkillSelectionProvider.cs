using System;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using StateMachine.BattleMenu;
using UniRx;

namespace Gameplay.Combat.SkillSelection
{
    public class PlayerSkillSelectionProvider : SkillSelectionProvider
    {
        private readonly BattleMenuStateMachine _battleMenuStateMachine;
        
        public PlayerSkillSelectionProvider(UnitSkillsData unitSkillsData, BattleMenuStateMachine battleMenuStateMachine) : base(unitSkillsData)
        {
            _battleMenuStateMachine = battleMenuStateMachine;
        }

        public override async UniTask<BaseSkill> SelectSkillToUse()
        {
            var tcs = new UniTaskCompletionSource<BaseSkill>();

            _battleMenuStateMachine.OpenBattleMenu();
            
            IDisposable disposable = _battleMenuStateMachine.SkillSelected.Subscribe(skill => tcs.TrySetResult(skill));

            BaseSkill selectedSkill = await tcs.Task;
            
            disposable.Dispose();

            return selectedSkill;
        }
    }
}