using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Shop;
using UI.BattleMenu;

namespace StateMachine.Shop
{
    public class SkillsShopState : ItemsShopState
    {
        public SkillsShopState(
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
            var itemsSelection = ShopItemsProvider.CreateSkillsForSale();

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