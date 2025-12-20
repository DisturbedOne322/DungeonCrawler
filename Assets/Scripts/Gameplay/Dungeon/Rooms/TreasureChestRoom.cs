using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Animations;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.Rooms
{
    [RequireComponent(typeof(ChestAnimator))]
    public class TreasureChestRoom : StopRoom
    {
        [SerializeField] private ChestAnimator _chestAnimator;
        
        private PlayerUnit _playerUnit;
        
        private TreasureChestRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;

        public void SetData(TreasureChestRoomVariantData data) => _roomData = data;

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