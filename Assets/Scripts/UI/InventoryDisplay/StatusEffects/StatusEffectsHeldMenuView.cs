using Zenject;

namespace UI.InventoryDisplay
{
    public class StatusEffectsHeldMenuView : BaseInventoryMenuView
    {
        private StatusEffectsHeldViewFactory _factory;

        [Inject]
        public void Construct(StatusEffectsHeldDisplayMenu displayMenu, StatusEffectsHeldViewFactory factory)
        {
            DisplayMenu = displayMenu;
            _factory = factory;
        }

        protected override void Initialize()
        {
            var menu = DisplayMenu.CreateMenu();
            var views = _factory.CreateViews(menu.Items);
            MenuPageView.SetItems(views, menu.ItemsUpdater);
        }
    }
}