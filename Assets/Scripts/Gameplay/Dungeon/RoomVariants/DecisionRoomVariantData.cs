using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "DecisionRoomVariantData")]
    public class DecisionRoomVariantData : RoomVariantData
    {
        [SerializeField] [Range(1, 3)] private int _roomsForSelection = 3;
        public override RoomType RoomType => RoomType.Decision;

        public int RoomsForSelection => _roomsForSelection;
    }
}