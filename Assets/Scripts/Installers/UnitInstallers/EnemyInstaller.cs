using Gameplay.Combat.SkillSelection;
using Zenject;

namespace Installers.UnitInstallers
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BaseActionSelectionProvider>().To<AIBaseActionSelectionProvider>().AsSingle();
        }
    }
}