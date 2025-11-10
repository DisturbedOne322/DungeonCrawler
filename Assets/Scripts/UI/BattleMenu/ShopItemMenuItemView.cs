using Gameplay.Dungeon.Rooms.BaseSellableItems;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleMenu
{
    public class ShopItemMenuItemView : BaseMenuItemView
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _priceText;

        public void SetData(SoldItemMenuItemData data)
        {
            _image.sprite = data.SoldItemModel.ItemData.Item.Icon;
            _descText.SetText(data.Description);
            _priceText.SetText(data.SoldItemModel.ItemData.Price.ToString());

            data.SoldItemModel.AmountLeft.Subscribe(UpdateCountText).AddTo(Disposables);
        }

        private void UpdateCountText(int amount)
        {
            _amountText.SetText("x" + amount);
        }
    }
}