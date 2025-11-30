using UI.Menus;
using Zenject;

namespace UI.InventoryDisplay.Skills
{
    public class SkillsHeldMenuView : BaseInventoryMenuView
    {
        private MenuItemViewFactory _factory;

        [Inject]
        public void Construct(SkillsHeldMenu menu, MenuItemViewFactory factory)
        {
            DisplayMenu = menu;
            _factory = factory;
        }

        protected override void Initialize()
        {
            var menu = DisplayMenu.CreateMenu();

            var views = _factory.CreateSkillMenuItems(menu.Items);
            MenuPageView.SetItems(views, menu.ItemsUpdater);
            
            base.Initialize();
        }
    }
}