using Data;
using Gameplay.Dungeon.Rooms.SkillRoom;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "MagicMasterRoomVariantData", menuName = "Gameplay/Dungeon/Data/MagicMasterRoomVariantData")]
    public class MagicMasterRoomVariantData : RoomVariantData
    {
        [SerializeField] private MagicSkillsMasterConfig _config;
        public MagicSkillsMasterConfig Config => _config;
        
        public override RoomType RoomType => RoomType.MagicMaster;
    }
}