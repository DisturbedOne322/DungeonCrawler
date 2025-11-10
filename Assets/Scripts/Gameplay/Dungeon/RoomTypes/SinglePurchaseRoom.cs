using Cysharp.Threading.Tasks;
using PopupControllers;
using Zenject;

namespace Gameplay.Dungeon.RoomTypes
{
    public abstract class SinglePurchaseRoom : StopRoom
    {
        private SingleTypePurchaseController _shopController;

        [Inject]
        private void Construct(SingleTypePurchaseController shopController)
        {
            _shopController = shopController;
        }

        public override async UniTask ClearRoom()
        {
            await _shopController.RunShop();
        }
    }
}