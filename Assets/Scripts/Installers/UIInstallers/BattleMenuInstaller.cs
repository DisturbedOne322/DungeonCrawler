using UI.PlayerBattleMenu;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers.UIInstallers
{
    public class BattleMenuInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_battleMenuController")] [SerializeField] private BattleMenuView _battleMenuView;
        [SerializeField] private MenuItemViewFactory _menuItemViewFactory;

        public override void InstallBindings()
        {
            Container.Bind<BattleMenuView>().FromInstance(_battleMenuView).AsSingle();
            Container.Bind<MenuItemViewFactory>().FromInstance(_menuItemViewFactory).AsSingle();
        }
    }
}