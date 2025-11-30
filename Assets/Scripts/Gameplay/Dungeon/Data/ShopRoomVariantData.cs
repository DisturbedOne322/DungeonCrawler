using Data;
using Gameplay.Dungeon.Rooms.Shop;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "ShopRoomVariantData", menuName = "Gameplay/Dungeon/Data/ShopRoomVariantData")] 
    public class ShopRoomVariantData : RoomVariantData
    {
        [SerializeField] private ShopItemsConfig _config;
        public ShopItemsConfig Config => _config;
        
        public override RoomType RoomType => RoomType.Shop;
    }
}