using Data;
using Data.Constants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Dungeon.ShopRooms.SkillRoom;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "MagicMasterRoomVariantData")]
    public class MagicMasterRoomVariantData : BaseSpecialtyShopRoomVariantData
    {
        [SerializeField] private MagicSkillsMasterConfig _config;
        public override RoomType RoomType => RoomType.MagicMaster;
        public override BasePurchasableItemsConfig Config => _config;
    }
}