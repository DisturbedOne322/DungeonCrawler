using Data;
using Gameplay.Dungeon.Rooms.SkillRoom;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "PhysicalMasterRoomVariantData", menuName = "Gameplay/Dungeon/Data/PhysicalMasterRoomVariantData")]
    public class PhysicalMasterRoomVariantData : RoomVariantData
    {
        [SerializeField] private PhysicalSkillsMasterConfig _config;
        
        public PhysicalSkillsMasterConfig Config => _config;
        
        public override RoomType RoomType => RoomType.PhysicalMaster;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var physRoom = room as PhysicalMasterRoom;
            physRoom?.SetData(this);
        }
    }
}