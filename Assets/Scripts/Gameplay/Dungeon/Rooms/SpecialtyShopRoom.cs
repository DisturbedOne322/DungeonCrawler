using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using Installers.ServiceInstallers;
using PopupControllers;
using UnityEngine;
using Zenject;

namespace Gameplay.Dungeon.Rooms
{
    [RequireComponent(typeof(SpecialtyShopRoomInstaller))]
    public abstract class SpecialtyShopRoom : VariantRoom<BaseSpecialtyShopRoomVariantData>
    {
        private SingleTypePurchaseController _shopController;

        public BasePurchasableItemsConfig Config => RoomVariantData.Config;

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