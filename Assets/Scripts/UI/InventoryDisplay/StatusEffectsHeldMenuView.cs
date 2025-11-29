using System;
using UI.Menus;
using UnityEngine;
using Zenject;

namespace UI.InventoryDisplay
{
    public class StatusEffectsHeldMenuView : MonoBehaviour
    {
        [SerializeField] private MenuPageView _menuPageView;

        private StatusEffectsHeldMenuController _menuController;
        private StatusEffectsHeldViewFactory _factory;

        [Inject]
        public void Construct(StatusEffectsHeldMenuController menuController, StatusEffectsHeldViewFactory factory)
        {
            _menuController = menuController;
            _factory = factory;
        }

        private void Start()
        {
            _menuController.CreateMenu();
            
            var itemsList = _menuController.Items;
            var itemsUpdater = _menuController.ItemsUpdater;

            var views = _factory.CreateViews(itemsList);
            _menuPageView.SetItems(views, itemsUpdater);
            
            _menuController.TakeControls();
        }

        private void OnDestroy()
        {
            _menuController.RemoveControls();
        }
    }
}