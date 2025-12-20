using Data;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "DecisionRoomVariantData", menuName = "Gameplay/Dungeon/Data/DecisionRoomVariantData")]
    public class DecisionRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.Decision;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var decisionRoom = room as DecisionRoom;
            decisionRoom?.SetData(this);
        }
    }
}