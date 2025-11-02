using Cysharp.Threading.Tasks;
using Gameplay;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Shop;
using Gameplay.Units;
using UI.BattleMenu;
using UniRx;

namespace StateMachine.Shop
{
    public abstract class ItemsShopState : BaseShopState
    {
        protected readonly ShopItemsProvider ShopItemsProvider;
        protected readonly BalanceService BalanceService;
        private readonly RewardDistributor _rewardDistributor;
        
        protected ItemsShopState(
            PlayerInputProvider playerInputProvider, 
            MenuItemsUpdater menuItemsUpdater,
            ShopItemsProvider shopItemsProvider,
            BalanceService balanceService,
            RewardDistributor rewardDistributor) : 
            base(playerInputProvider, menuItemsUpdater)
        {
            ShopItemsProvider = shopItemsProvider;
            BalanceService = balanceService;
            _rewardDistributor = rewardDistributor;
        }
        
        protected override void SubscribeToInputEvents()
        {
            Disposables = new ();

            Disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ => MenuItemsUpdater.ExecuteSelection()));
            Disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ => MenuItemsUpdater.UpdateSelection(-1)));
            Disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ => MenuItemsUpdater.UpdateSelection(+1)));
            Disposables.Add(PlayerInputProvider.OnUiBack.Subscribe(_ => StateMachine.GoToPrevState().Forget()));
        }
        
        protected async UniTask PurchaseItem(ShopItemModel model)
        {
            await _rewardDistributor.GiveRewardToPlayer(model.ItemData.Item, 1);
            BalanceService.AddBalance(-model.ItemData.Price);
            model.DecreaseAmount(1);
        }
        
        protected virtual bool IsSelectable(ShopItemModel model)
        {
            return BalanceService.HasEnoughBalance(model.ItemData.Price) && model.AmountLeft.Value > 0;
        }
    }
}