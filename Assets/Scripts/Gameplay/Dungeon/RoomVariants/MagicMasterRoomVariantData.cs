using Data;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Dungeon.ShopRooms.SkillRoom;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "MagicMasterRoomVariantData", menuName = "Gameplay/Dungeon/Data/MagicMasterRoomVariantData")]
    public class MagicMasterRoomVariantData : BaseSpecialtyShopRoomVariantData
    {
        [SerializeField] private MagicSkillsMasterConfig _config;
        public override BasePurchasableItemsConfig Config => _config;
        
        public override RoomType RoomType => RoomType.MagicMaster;
    }
}