using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Dungeon.Rooms.Shop;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using UI.BattleMenu;

namespace StateMachine.Shop
{
    public class ConsumablesShopState : ItemsShopState
    {
        public ConsumablesShopState(
            PlayerInputProvider playerInputProvider, 
            MenuItemsUpdater menuItemsUpdater, 
            ShopItemsProvider shopItemsProvider, 
            BalanceService balanceService, 
            RewardDistributor rewardDistributor) : 
            base(playerInputProvider, menuItemsUpdater, shopItemsProvider, balanceService, rewardDistributor)
        {
        }

        public override void InitMenuItems()
        {
            var itemsSelection = ShopItemsProvider.CreateConsumablesForSale();

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
    }
}