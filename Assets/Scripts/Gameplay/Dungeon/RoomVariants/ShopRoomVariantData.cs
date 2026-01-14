using Data;
using Data.Constants;
using Gameplay.Dungeon.ShopRooms.Shop;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "ShopRoomVariantData")]
    public class ShopRoomVariantData : RoomVariantData
    {
        [SerializeField] private ShopItemsConfig _config;
        public override RoomType RoomType => RoomType.Shop;
        public ShopItemsConfig Config => _config;
    }
}