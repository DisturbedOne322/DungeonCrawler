using Cysharp.Threading.Tasks;
using Data;
using PopupControllers;
using Zenject;

namespace Gameplay.Dungeon.RoomTypes
{
    public class ShopRoom : StopRoom
    {
        private ShopController _shopController;
        public override RoomType RoomType => RoomType.Shop;

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