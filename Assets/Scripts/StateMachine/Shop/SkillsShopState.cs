using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Shop;
using Gameplay.Units;
using Helpers;
using UI.BattleMenu;

namespace StateMachine.Shop
{
    public class SkillsShopState : ItemsShopState
    {
        private readonly PlayerUnit _player;
        public SkillsShopState(
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

        protected override bool IsSelectable(ShopItemModel model)
        {
            bool canPurchase = base.IsSelectable(model);
            bool isAlreadyPurchased = UnitInventoryHelper.HasSkill(_player, model.ItemData.Item);
            
            return canPurchase && !isAlreadyPurchased;
        }
    }
}