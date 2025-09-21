using UI.PlayerBattleMenu;
using UnityEngine;
using Zenject;

namespace Installers.UIInstallers
{
    public class BattleMenuInstaller : MonoInstaller
    {
        [SerializeField] private BattleMenuController _battleMenuController;
        [SerializeField] private MenuItemViewFactory _menuItemViewFactory;

        public override void InstallBindings()
        {
            Container.Bind<BattleMenuController>().FromInstance(_battleMenuController).AsSingle();
            Container.Bind<MenuItemViewFactory>().FromInstance(_menuItemViewFactory).AsSingle();
        }
    }
}