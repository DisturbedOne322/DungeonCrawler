using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Dungeon.ShopRooms.Shop;
using PopupControllers;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    public class ShopRoom : StopRoom
    {
        private ShopController _shopController;

        private ShopRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;

        public ShopItemsConfig Config => _roomData.Config;
        
        [Inject]
        private void Construct(ShopController shopController)
        {
            _shopController = shopController;
        }

        public override async UniTask ClearRoom()
        {
            await _shopController.RunShop();
        }

        public void SetData(ShopRoomVariantData data) => _roomData = data;
    }
}