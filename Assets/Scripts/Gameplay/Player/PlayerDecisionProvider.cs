using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
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
        
        public async UniTask<int> MakeDecision() => await WaitForPlayerInput();

        private async UniTask<int> WaitForPlayerInput()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            
            _playerInputProvider.EnableInput(true);
            
            var leftTask = _playerInputProvider.OnGoLeft.First().ToUniTask(cancellationToken: cts.Token);
            var forwardTask = _playerInputProvider.OnGoForward.First().ToUniTask(cancellationToken: cts.Token);
            var rightTask = _playerInputProvider.OnGoRight.First().ToUniTask(cancellationToken: cts.Token);
            
            var completed = await UniTask.WhenAny(leftTask, forwardTask, rightTask);

            cts.Cancel();
            cts.Dispose();
            
            _playerInputProvider.EnableInput(false);
            
            return completed.winArgumentIndex;
        }
    }
}