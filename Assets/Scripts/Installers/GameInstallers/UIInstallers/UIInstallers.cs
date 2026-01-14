using UI;
using UI.Core;
using UI.InventoryDisplay.Consumables;
using UI.InventoryDisplay.Skills;
using UI.InventoryDisplay.StatusEffects;
using UI.Menus;
using UI.Navigation;
using UnityEngine;
using Zenject;

namespace Installers.GameInstallers.UIInstallers
{
    public class UIInstallers : MonoInstaller
    {
        [SerializeField] private UIFactory _uiFactoryPrefab;

        public override void InstallBindings()
        {
            Container.Bind<MenuItemViewFactory>().AsSingle();

            Container.Bind<HorizontalUINavigator>().AsTransient();
            Container.Bind<VerticalUiNavigator>().AsTransient();

            Container.Bind<UIFactory>().FromComponentInNewPrefab(_uiFactoryPrefab).AsSingle();
            Container.Bind<PopupsRegistry>().AsSingle();
            Container.Bind<MenuItemPrefabsRegistry>().AsSingle();

            Container.Bind<ConsumablesHeldMenu>().AsSingle();
            Container.Bind<SkillsHeldMenu>().AsSingle();
            Container.Bind<StatusEffectsHeldDisplayMenu>().AsSingle();
            Container.Bind<StatusEffectsHeldViewFactory>().AsSingle();
        }
    }
}