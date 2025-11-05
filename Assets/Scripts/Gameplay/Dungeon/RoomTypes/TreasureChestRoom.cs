using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon.Animations;
using UnityEngine;

namespace Gameplay.Dungeon.RoomTypes
{
    [RequireComponent(typeof(ChestAnimator))]
    public class TreasureChestRoom : StopRoom
    {
        [SerializeField] private ChestAnimator _chestAnimator;
        
        public override RoomType RoomType => RoomType.TreasureChest;
        
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