using Data;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Gameplay.Dungeon.ShopRooms.Shrine;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "ShrineRoomVariantData", menuName = "Gameplay/Dungeon/Data/ShrineRoomVariantData")] 
    public class ShrineRoomVariantData : BaseSpecialtyShopRoomVariantData
    {
        [SerializeField] private ShrineBuffsConfig _config;
        public override BasePurchasableItemsConfig Config => _config;

        public override RoomType RoomType => RoomType.Shrine;
    }
}