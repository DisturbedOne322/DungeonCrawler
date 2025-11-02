using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Shop;
using UI.BattleMenu;
using UniRx;
using UnityEngine;

namespace StateMachine.Shop
{
    public abstract class ItemsShopState : BaseShopState
    {
        protected readonly ShopItemsProvider ShopItemsProvider;
        protected readonly BalanceService BalanceService;
        private readonly RewardDistributor _rewardDistributor;

        private bool _isInputLocked = false;
        
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

            Disposables.Add(PlayerInputProvider.OnUiSubmit.Subscribe(_ =>
            {
                if(!_isInputLocked)
                    MenuItemsUpdater.ExecuteSelection();
            }));
            
            Disposables.Add(PlayerInputProvider.OnUiUp.Subscribe(_ =>
            {
                if(!_isInputLocked)
                    MenuItemsUpdater.UpdateSelection(-1);
            }));
            
            Disposables.Add(PlayerInputProvider.OnUiDown.Subscribe(_ =>
            {
                if(!_isInputLocked)
                    MenuItemsUpdater.UpdateSelection(+1);
            }));
            
            Disposables.Add(PlayerInputProvider.OnUiBack.Subscribe(_ =>
            {
                Debug.Log("BACK!!!!");
                if(!_isInputLocked)
                    StateMachine.GoToPrevState().Forget();
            }));
        }
        
        protected async UniTask PurchaseItem(ShopItemModel model)
        {
            _isInputLocked = true;
            await _rewardDistributor.GiveRewardToPlayer(model.ItemData.Item, 1);

            BalanceService.AddBalance(-model.ItemData.Price);
            model.DecreaseAmount(1);

            _isInputLocked = false;
        }
        
        protected virtual bool IsSelectable(ShopItemModel model)
        {
            return BalanceService.HasEnoughBalance(model.ItemData.Price) && model.AmountLeft.Value > 0;
        }
    }
}