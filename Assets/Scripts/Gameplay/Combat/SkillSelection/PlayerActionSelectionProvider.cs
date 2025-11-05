using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Player;
using Gameplay.Units;
using StateMachine.BattleMenu;
using UniRx;

namespace Gameplay.Combat.SkillSelection
{
    public class PlayerActionSelectionProvider : ActionSelectionProvider
    {
        private readonly BattleMenuStateMachine _battleMenuStateMachine;
        private readonly PlayerInputProvider _playerInputProvider;

        public PlayerActionSelectionProvider(UnitSkillsData unitSkillsData, UnitInventoryData unitInventoryData,
            BattleMenuStateMachine battleMenuStateMachine, PlayerInputProvider playerInputProvider) : base(unitSkillsData, unitInventoryData)
        {
            _battleMenuStateMachine = battleMenuStateMachine;
            _playerInputProvider = playerInputProvider;
        }

        public override async UniTask<BaseCombatAction> SelectAction()
        {
            var tcs = new UniTaskCompletionSource<BaseCombatAction>();

            _battleMenuStateMachine.OpenBattleMenu();

            var disposable = _battleMenuStateMachine.ActionSelected.Subscribe(action => tcs.TrySetResult(action));

            var result = await _playerInputProvider.EnableUIInputUntil(tcs.Task);
            
            disposable.Dispose();

            return result;
        }
    }
}