using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using UI.BattleMenu;
using UniRx;

namespace Gameplay.Dungeon.Rooms.BaseSellableItems
{
    public class ItemSellingController
    {
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly SellableItemsProvider _sellableItemsProvider;
        private readonly RewardDistributor _rewardDistributor;
        private readonly BalanceService _balanceService;
        private readonly MenuItemsUpdater _menuItemsUpdater;
        
        private UniTaskCompletionSource _cts = new();
        private CompositeDisposable _disposables = new();
        
        private bool _isInputLocked;
        
        public ItemSellingController(PlayerInputProvider playerInputProvider, 
            SellableItemsProvider sellableItemsProvider,
            RewardDistributor rewardDistributor,
            BalanceService balanceService,
            MenuItemsUpdater menuItemsUpdater)
        {
            _playerInputProvider = playerInputProvider;
            _sellableItemsProvider = sellableItemsProvider;
            _rewardDistributor = rewardDistributor;
            _balanceService = balanceService;
            _menuItemsUpdater = menuItemsUpdater;
        }

        public void Initialize()
        {
            _cts = new();
            InitItems();
            SubscribeInput();
        }
        
        public async UniTask ProcessSelling()
        {
            await _cts.Task;
            _disposables.Dispose();
        }
        
        private void InitItems()
        {
            var itemsSelection = _sellableItemsProvider.GetSellableItems();

            List<MenuItemData> items = new ();
            
            foreach (var model in itemsSelection)
            {
                items.Add(
                    new SoldItemMenuItemData(
                        model,
                        () => IsSelectable(model), 
                        () => PurchaseItem(model).Forget()
                    ));
            }
            
            _menuItemsUpdater.SetMenuItems(items);
        }

        private void SubscribeInput()
        {
            _disposables = new();
            _disposables.Add(_playerInputProvider.OnUiSubmit.Subscribe(_ =>
            {
                if(!_isInputLocked)
                    _menuItemsUpdater.ExecuteSelection();
            }));
            
            _disposables.Add(_playerInputProvider.OnUiUp.Subscribe(_ =>
            {
                if(!_isInputLocked)
                    _menuItemsUpdater.UpdateSelection(-1);
            }));
            
            _disposables.Add(_playerInputProvider.OnUiDown.Subscribe(_ =>
            {
                if(!_isInputLocked)
                    _menuItemsUpdater.UpdateSelection(+1);
            }));
            
            _disposables.Add(_playerInputProvider.OnUiBack.Subscribe(_ =>
            {
                if (!_isInputLocked)
                    _cts.TrySetResult();
            }));
        }
        
        private async UniTask PurchaseItem(SoldItemModel model)
        {
            _isInputLocked = true;
            await _rewardDistributor.GiveRewardToPlayer(model.ItemData.Item, 1);

            _balanceService.AddBalance(-model.ItemData.Price);
            model.DecreaseAmount(1);

            _isInputLocked = false;
        }
        
        private bool IsSelectable(SoldItemModel model)
        {
            return _balanceService.HasEnoughBalance(model.ItemData.Price) && model.AmountLeft.Value > 0;
        }
    }
}