using UI;
using UI.BattleMenu;
using UI.Core;
using UI.InventoryDisplay;
using UI.Menus;
using UI.Navigation;
using Zenject;

namespace Installers.GameInstallers.UIInstallers
{
    public class UIInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuItemViewFactory>().AsSingle();

            Container.Bind<HorizontalUINavigator>().AsTransient();
            Container.Bind<VerticalUiNavigator>().AsTransient();

            Container.Bind<UIFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PopupsRegistry>().AsSingle();
            Container.Bind<MenuItemPrefabsRegistry>().AsSingle();
            
            Container.Bind<StatusEffectsHeldMenuController>().AsSingle();
            Container.Bind<StatusEffectsHeldViewFactory>().AsSingle();
        }
    }
}