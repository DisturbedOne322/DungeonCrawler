using Data;
using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.ShopRooms.SkillRoom;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "MagicMasterRoomVariantData", menuName = "Gameplay/Dungeon/Data/MagicMasterRoomVariantData")]
    public class MagicMasterRoomVariantData : RoomVariantData
    {
        [SerializeField] private MagicSkillsMasterConfig _config;
        public MagicSkillsMasterConfig Config => _config;
        
        public override RoomType RoomType => RoomType.MagicMaster;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var magicRoom = room as MagicMasterRoom;
            magicRoom?.SetData(this);
        }
    }
}