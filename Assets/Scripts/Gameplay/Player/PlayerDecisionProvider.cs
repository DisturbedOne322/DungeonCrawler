using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data.Constants;
using UniRx;

namespace Gameplay.Player
{
    public class PlayerDecisionProvider
    {
        private readonly PlayerInputProvider _playerInputProvider;

        public PlayerDecisionProvider(PlayerInputProvider playerInputProvider)
        {
            _playerInputProvider = playerInputProvider;
        }

        public async UniTask<int> MakeDecision(int selectionCount)
        {
            return await WaitForPlayerInput(selectionCount);
        }

        private async UniTask<int> WaitForPlayerInput(int selectionCount)
        {
            var cts = new CancellationTokenSource();

            _playerInputProvider.EnableMovementInput(true);
            
            var completed = await UniTask.WhenAny(CreateInputTasks(selectionCount, cts.Token));

            cts.Cancel();
            cts.Dispose();

            _playerInputProvider.EnableMovementInput(false);

            return completed;
        }

        private List<UniTask> CreateInputTasks(int selection, CancellationToken token)
        {
            List<UniTask> inputTasks = new();

            switch (selection)
            {
                case 1:
                    inputTasks.Add(_playerInputProvider.OnGoForward.First().ToUniTask(cancellationToken: token));
                    break;
                case 2:
                    inputTasks.Add(_playerInputProvider.OnGoLeft.First().ToUniTask(cancellationToken: token));
                    inputTasks.Add(_playerInputProvider.OnGoRight.First().ToUniTask(cancellationToken: token));
                    break;
                case 3:
                    inputTasks.Add(_playerInputProvider.OnGoLeft.First().ToUniTask(cancellationToken: token));
                    inputTasks.Add(_playerInputProvider.OnGoForward.First().ToUniTask(cancellationToken: token));
                    inputTasks.Add(_playerInputProvider.OnGoRight.First().ToUniTask(cancellationToken: token));
                    break;
            }
            
            return inputTasks;
        }
    }
}