using TMPro;
using UI.BattleMenu;
using UI.Core;
using UnityEngine;
using Zenject;

namespace UI.Gameplay
{
    public class SingleTypePurchasePopup : BasePopup
    {
        [SerializeField] private TextMeshProUGUI _shopNameText;
        [SerializeField] private RectTransform _pageParent;

        private MenuItemsUpdater _menuItemsUpdater;
        private MenuItemViewFactory _menuItemViewFactory;

        [Inject]
        private void Construct(MenuItemViewFactory menuItemViewFactory)
        {
            _menuItemViewFactory = menuItemViewFactory;
        }

        public void Initialize(MenuItemsUpdater menuItemsUpdater, string popupName)
        {
            _menuItemsUpdater = menuItemsUpdater;
            CreateMenu();

            SetName(popupName);
        }

        public void SetName(string shopName)
        {
            _shopNameText.text = shopName;
        }

        private void CreateMenu()
        {
            var page = _menuItemViewFactory.CreatePage();
            page.transform.SetParent(_pageParent, false);
            var items = _menuItemViewFactory.CreateViewsForData(_menuItemsUpdater.MenuItems);
            page.SetItems(items, _menuItemsUpdater);
        }
    }
}