using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using UniRx;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerDecisionProvider
    {
        private readonly PlayerInputProvider _playerInputProvider;

        public PlayerDecisionProvider(PlayerInputProvider playerInputProvider)
        {
            _playerInputProvider = playerInputProvider;
        }
        
        public async UniTask<RoomType> MakeDecision(List<RoomType> availableRooms)
        {
            _playerInputProvider.EnableInput(true);
            
            var leftTask = _playerInputProvider.OnGoLeft.First().ToUniTask();
            var forwardTask = _playerInputProvider.OnGoForward.First().ToUniTask();
            var rightTask = _playerInputProvider.OnGoRight.First().ToUniTask();

            var completed = await UniTask.WhenAny(leftTask, forwardTask, rightTask);

            _playerInputProvider.EnableInput(false);
            
            return completed.winArgumentIndex switch
            {
                0 when availableRooms.Count > 0 => availableRooms[0],
                1 when availableRooms.Count > 1 => availableRooms[1],
                2 when availableRooms.Count > 2 => availableRooms[2],
                _ => availableRooms[0]
            };
        }
    }
}