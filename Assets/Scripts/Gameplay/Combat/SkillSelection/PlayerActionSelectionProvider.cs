using System;
using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using StateMachine.BattleMenu;
using UniRx;

namespace Gameplay.Combat.SkillSelection
{
    public class PlayerActionSelectionProvider : ActionSelectionProvider
    {
        private readonly BattleMenuStateMachine _battleMenuStateMachine;
        
        public PlayerActionSelectionProvider(UnitSkillsData unitSkillsData, UnitInventoryData unitInventoryData,
            BattleMenuStateMachine battleMenuStateMachine) : base(unitSkillsData, unitInventoryData)
        {
            _battleMenuStateMachine = battleMenuStateMachine;
        }

        public override async UniTask<BaseCombatAction> SelectAction()
        {
            var tcs = new UniTaskCompletionSource<BaseCombatAction>();

            _battleMenuStateMachine.OpenBattleMenu();
            
            IDisposable disposable = _battleMenuStateMachine.ActionSelected.Subscribe(action => tcs.TrySetResult(action));

            BaseCombatAction selectedSkill = await tcs.Task;
            
            disposable.Dispose();

            return selectedSkill;
        }
    }
}