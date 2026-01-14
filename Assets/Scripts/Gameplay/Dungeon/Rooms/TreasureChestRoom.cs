using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Animations;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms
{
    [RequireComponent(typeof(ChestAnimator))]
    public class TreasureChestRoom : VariantRoom<TreasureChestRoomVariantData>
    {
        [SerializeField] private ChestAnimator _chestAnimator;

        private PlayerUnit _playerUnit;

        public override void ResetRoom()
        {
            _chestAnimator.ResetChest();
        }

        public override async UniTask ClearRoom()
        {
            await _chestAnimator.PlayOpenAnimation();
        }
    }
}