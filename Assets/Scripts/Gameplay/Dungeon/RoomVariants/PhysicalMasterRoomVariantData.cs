using Data;
using Data.Constants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Dungeon.ShopRooms.SkillRoom;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "PhysicalMasterRoomVariantData")]
    public class PhysicalMasterRoomVariantData : BaseSpecialtyShopRoomVariantData
    {
        [SerializeField] private PhysicalSkillsMasterConfig _config;
        public override RoomType RoomType => RoomType.PhysicalMaster;
        public override BasePurchasableItemsConfig Config => _config;
    }
}