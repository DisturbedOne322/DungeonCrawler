using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Dungeon.Rooms.Shop;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Units;
using Helpers;
using UI.BattleMenu;

namespace StateMachine.Shop
{
    public class EquipmentShopState : ItemsShopState
    {
        private readonly PlayerUnit _player;

        public EquipmentShopState(
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater,
            ShopItemsProvider shopItemsProvider,
            BalanceService balanceService,
            ItemsDistributor itemsDistributor,
            PlayerUnit player) :
            base(playerInputProvider, menuItemsUpdater, shopItemsProvider, balanceService, itemsDistributor)
        {
            _player = player;
        }

        public override void InitMenuItems()
        {
            var itemsSelection = ShopItemsProvider.CreateEquipmentForSale();

            foreach (var model in itemsSelection)
                MenuItems.Add(
                    new PurchasedItemMenuItemData(
                        model,
                        () => IsSelectable(model),
                        () => PurchaseItem(model).Forget()
                    ));
        }

        protected override bool IsSelectable(PurchasedItemModel model)
        {
            var purchasable = base.IsSelectable(model);
            var isAlreadyPurchased =
                UnitInventoryHelper.HasItem(_player, model.ItemData.Item);

            return purchasable && !isAlreadyPurchased;
        }
    }
}