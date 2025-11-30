using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon.Data;
using PopupControllers;
using Zenject;

namespace Gameplay.Dungeon.RoomTypes
{
    public class ShopRoom : StopRoom
    {
        private ShopController _shopController;

        private ShopRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;

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