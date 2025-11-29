using UI.Menus;
using Zenject;

namespace UI.InventoryDisplay.Consumables
{
    public class ConsumablesMenuView : BaseInventoryMenuView
    {
        private MenuItemViewFactory _factory;
        
        [Inject]
        public void Construct(ConsumablesHeldMenu heldMenu, MenuItemViewFactory factory)
        {
            DisplayMenu = heldMenu;
            _factory = factory;
        }
        
        protected override void Initialize()
        {
            var menu = DisplayMenu.CreateMenu();

            var views = _factory.CreateConsumableMenuItems(menu.Items);
            MenuPageView.SetItems(views, menu.ItemsUpdater);
        }
    }
}