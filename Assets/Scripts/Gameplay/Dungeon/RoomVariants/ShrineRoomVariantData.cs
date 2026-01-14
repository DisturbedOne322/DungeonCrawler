using Data;
using Data.Constants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Dungeon.ShopRooms.Shrine;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "ShrineRoomVariantData")]
    public class ShrineRoomVariantData : BaseSpecialtyShopRoomVariantData
    {
        [SerializeField] private ShrineBuffsConfig _config;
        public override RoomType RoomType => RoomType.Shrine;
        public override BasePurchasableItemsConfig Config => _config;
    }
}