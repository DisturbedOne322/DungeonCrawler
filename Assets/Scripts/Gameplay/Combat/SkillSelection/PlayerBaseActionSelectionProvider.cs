using Cysharp.Threading.Tasks;
using Gameplay.Facades;
using StateMachine.BattleMenu;
using UniRx;

namespace Gameplay.Combat.SkillSelection
{
    public class PlayerBaseActionSelectionProvider : BaseActionSelectionProvider
    {
        private readonly BattleMenuStateMachine _battleMenuStateMachine;

        public PlayerBaseActionSelectionProvider(IGameUnit unit,
            BattleMenuStateMachine battleMenuStateMachine) : base(unit)
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