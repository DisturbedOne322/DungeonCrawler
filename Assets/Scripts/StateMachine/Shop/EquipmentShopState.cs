using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Shop;
using Gameplay.Units;
using UI.BattleMenu;

namespace StateMachine.Shop
{
    public class EquipmentShopState : ItemsShopState
    {
        public EquipmentShopState( 
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
            var itemsSelection = ShopItemsProvider.CreateEquipmentForSale();

            foreach (var model in itemsSelection)
            {
                MenuItems.Add(
                    new ShopMenuItemData(
                        model,
                        () => IsSelectable(model), 
                        () => PurchaseItem(model).Forget()
                    ));
            }
        }
    }
}