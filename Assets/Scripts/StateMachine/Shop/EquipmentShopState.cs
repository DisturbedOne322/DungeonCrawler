using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
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
            RewardDistributor rewardDistributor,
            PlayerUnit player) : 
            base(playerInputProvider, menuItemsUpdater, shopItemsProvider, balanceService, rewardDistributor)
        {
            _player = player;
        }

        public override void InitMenuItems()
        {
            var itemsSelection = ShopItemsProvider.CreateEquipmentForSale();

            foreach (var model in itemsSelection)
            {
                MenuItems.Add(
                    new SoldItemMenuItemData(
                        model,
                        () => IsSelectable(model), 
                        () => PurchaseItem(model).Forget()
                    ));
            }
        }

        protected override bool IsSelectable(SoldItemModel model)
        {
            bool purchasable = base.IsSelectable(model);
            bool isAlreadyPurchased =
                UnitInventoryHelper.HasItem(_player, model.ItemData.Item);
            
            return purchasable && !isAlreadyPurchased;
        }
    }
}