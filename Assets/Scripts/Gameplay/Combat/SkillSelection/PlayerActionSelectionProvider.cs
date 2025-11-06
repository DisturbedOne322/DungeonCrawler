using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Units;
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

            var disposable = _battleMenuStateMachine.ActionSelected.Subscribe(action => tcs.TrySetResult(action));

            var result = await tcs.Task;
            
            disposable.Dispose();

            return result;
        }
    }
}