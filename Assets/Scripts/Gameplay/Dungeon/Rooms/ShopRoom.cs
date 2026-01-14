using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Dungeon.ShopRooms.Shop;
using PopupControllers;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    public class ShopRoom : VariantRoom<ShopRoomVariantData>
    {
        private ShopController _shopController;

        public ShopItemsConfig Config => RoomVariantData.Config;

        [Inject]
        private void Construct(ShopController shopController)
        {
            _shopController = shopController;
        }

        public override async UniTask ClearRoom()
        {
            await _shopController.RunShop();
        }
    }
}