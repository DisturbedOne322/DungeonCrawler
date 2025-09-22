using Gameplay.Combat.SkillSelection;
using StateMachine.BattleMenu;
using Zenject;

namespace Installers.UnitInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SkillSelectionProvider>().To<PlayerSkillSelectionProvider>().AsSingle();
            
            Container.Bind<BattleMenuStateMachine>().AsSingle();
            Container.Bind<BattleMenuState>().To<MainBattleMenuState>().AsSingle();
            Container.Bind<BattleMenuState>().To<SkillSelectBattleMenuState>().AsSingle();

            Container.Bind<MenuItemsUpdater>().AsTransient();
        }
    }
}