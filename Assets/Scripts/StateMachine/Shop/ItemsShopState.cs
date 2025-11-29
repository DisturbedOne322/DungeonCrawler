using Cysharp.Threading.Tasks;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Dungeon.Rooms.Shop;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using UI.BattleMenu;
using UI.Menus;

namespace StateMachine.Shop
{
    public abstract class ItemsShopState : BaseShopState
    {
        private readonly ItemsDistributor _itemsDistributor;
        protected readonly BalanceService BalanceService;
        protected readonly ShopItemsProvider ShopItemsProvider;

        private bool _isInputLocked;

        protected ItemsShopState(
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater,
            ShopItemsProvider shopItemsProvider,
            BalanceService balanceService,
            ItemsDistributor itemsDistributor) :
            base(playerInputProvider, menuItemsUpdater)
        {
            ShopItemsProvider = shopItemsProvider;
            BalanceService = balanceService;
            _itemsDistributor = itemsDistributor;
        }

        public override void OnUISubmit()
        {
            MenuItemsUpdater.ExecuteSelection();
        }

        public override void OnUIUp()
        {
            MenuItemsUpdater.UpdateSelection(-1);
        }

        public override void OnUIDown()
        {
            MenuItemsUpdater.UpdateSelection(+1);
        }

        public override void OnUIBack()
        {
            StateMachine.GoToPrevState().Forget();
        }

        protected async UniTask PurchaseItem(PurchasedItemModel model)
        {
            _isInputLocked = true;
            await _itemsDistributor.GiveRewardToPlayer(model.ItemData.Item, 1, ItemObtainContext.Purchase);

            BalanceService.AddBalance(-model.ItemData.Price);
            model.DecreaseAmount(1);

            _isInputLocked = false;
        }

        protected virtual bool IsSelectable(PurchasedItemModel model)
        {
            return BalanceService.HasEnoughBalance(model.ItemData.Price) && model.AmountLeft.Value > 0;
        }
    }
}