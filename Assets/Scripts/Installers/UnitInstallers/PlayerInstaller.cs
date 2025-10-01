using Gameplay.Combat.SkillSelection;
using StateMachine.BattleMenu;
using UI.BattleMenu;
using Zenject;

namespace Installers.UnitInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ActionSelectionProvider>().To<PlayerActionSelectionProvider>().AsSingle();

            BindBattleMenu();
        }

        private void BindBattleMenu()
        {
            Container.Bind<BattleMenuStateMachine>().AsSingle();
            Container.Bind<BattleMenuState>().To<MainBattleMenuState>().AsSingle();
            Container.Bind<BattleMenuState>().To<SkillSelectBattleMenuState>().AsSingle();
            Container.Bind<BattleMenuState>().To<ItemSelectBattleMenuState>().AsSingle();
            
            Container.Bind<MenuItemsUpdater>().AsTransient();
        }
    }
}