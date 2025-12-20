using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menus.MenuItemViews
{
    public class ShopItemMenuItemView : BaseMenuItemView
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _priceText;

        public void SetData(PurchasableItemMenuItemData data)
        {
            _image.sprite = data.PurchasedItemModel.ItemData.Item.Icon;
            _descText.SetText(data.Description);
            _priceText.SetText(data.PurchasedItemModel.ItemData.Price.ToString());

            data.PurchasedItemModel.AmountLeft.Subscribe(UpdateCountText).AddTo(Disposables);
        }

        private void UpdateCountText(int amount)
        {
            _amountText.SetText("x" + amount);
        }
    }
}