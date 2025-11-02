using Controllers;
using UI;
using UI.BattleMenu;
using UI.Navigation;
using Zenject;

namespace Installers.GameInstallers.UIInstallers
{
    public class UIInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EquipmentChangeController>().AsSingle();
            Container.Bind<SkillDiscardController>().AsSingle();
            Container.Bind<MenuItemViewFactory>().AsSingle();

            Container.Bind<HorizontalUINavigator>().AsTransient();
            Container.Bind<VerticalUiNavigator>().AsTransient();

            Container.Bind<UIFactory>().FromComponentInHierarchy().AsSingle();
        }
    }
}