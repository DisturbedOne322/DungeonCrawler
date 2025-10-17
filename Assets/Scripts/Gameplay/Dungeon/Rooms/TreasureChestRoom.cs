using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon.Animations;
using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    [RequireComponent(typeof(ChestAnimator))]
    public class TreasureChestRoom : StopRoom
    {
        [SerializeField] private ChestAnimator _chestAnimator;

        private BalanceService _balanceService;

        public override RoomType RoomType => RoomType.TreasureChest;

        [Inject]
        private void Construct(BalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public override void ResetRoom()
        {
            _chestAnimator.ResetChest();
        }

        public override async UniTask ClearRoom()
        {
            await _chestAnimator.PlayOpenAnimation();
            _balanceService.AddBalance(100);
        }
    }
}