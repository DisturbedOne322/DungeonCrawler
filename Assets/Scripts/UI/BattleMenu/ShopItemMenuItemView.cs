using System;
using Gameplay.Shop;
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
        
        public void SetData(ShopMenuItemData data)
        {
            _image.sprite = data.ShopItemModel.ItemData.Item.Icon;
            _descText.SetText(data.Description);
            _priceText.SetText(data.ShopItemModel.ItemData.Price.ToString());

            data.ShopItemModel.AmountLeft.Subscribe(UpdateCountText).AddTo(Disposables);
        }

        private void UpdateCountText(int amount)
        {
            _amountText.SetText("x" + amount);
        }
    }
}