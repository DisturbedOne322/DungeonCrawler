using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Units;
using Helpers;
using UI.Menus;
using UI.Menus.Data;

namespace Gameplay.Dungeon.ShopRooms.BasePurchasableItems
{
    public class ItemPurchaseController : BaseUIInputHandler
    {
        private readonly BalanceService _balanceService;
        private readonly ItemsDistributor _itemsDistributor;
        private readonly MenuItemsUpdater _menuItemsUpdater;
        private readonly PlayerUnit _player;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly PurchasableItemsProvider _purchasableItemsProvider;

        private UniTaskCompletionSource _cts = new();

        public ItemPurchaseController(PlayerInputProvider playerInputProvider,
            PurchasableItemsProvider purchasableItemsProvider,
            ItemsDistributor itemsDistributor,
            BalanceService balanceService,
            MenuItemsUpdater menuItemsUpdater,
            PlayerUnit player)
        {
            _playerInputProvider = playerInputProvider;
            _purchasableItemsProvider = purchasableItemsProvider;
            _itemsDistributor = itemsDistributor;
            _balanceService = balanceService;
            _menuItemsUpdater = menuItemsUpdater;
            _player = player;
        }

        public void Initialize()
        {
            _cts = new UniTaskCompletionSource();
            InitItems();
        }

        public async UniTask ProcessSelling()
        {
            await _playerInputProvider.EnableUIInputUntil(_cts.Task, this);
        }

        private void InitItems()
        {
            var itemsSelection = _purchasableItemsProvider.GetSellableItems();

            List<MenuItemData> items = new();

            foreach (var model in itemsSelection)
                items.Add(
                    new PurchasableItemMenuItemData(
                        model,
                        () => IsSelectable(model),
                        () => PurchaseItem(model).Forget()
                    ));

            _menuItemsUpdater.SetMenuItems(items);
        }

        public override void OnUISubmit()
        {
            _menuItemsUpdater.ExecuteSelection();
        }

        public override void OnUIUp()
        {
            _menuItemsUpdater.UpdateSelection(-1);
        }

        public override void OnUIDown()
        {
            _menuItemsUpdater.UpdateSelection(+1);
        }

        public override void OnUIBack()
        {
            _cts.TrySetResult();
        }

        private async UniTask PurchaseItem(PurchasedItemModel model)
        {
            await _itemsDistributor.GiveRewardToPlayer(model.ItemData.Item, 1, ItemObtainContext.Purchase);

            _balanceService.AddBalance(-model.ItemData.Price);
            model.DecreaseAmount(1);
        }

        private bool IsSelectable(PurchasedItemModel model)
        {
            var purchasable = _balanceService.HasEnoughBalance(model.ItemData.Price) && model.AmountLeft.Value > 0;
            var isAlreadyPurchased =
                UnitInventoryHelper.HasItem(_player, model.ItemData.Item);

            return purchasable && !isAlreadyPurchased;
        }
    }
}