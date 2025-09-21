using Gameplay.Combat.SkillSelection;
using Zenject;

namespace Installers.UnitInstallers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SkillSelectionProvider>().To<PlayerSkillSelectionProvider>().AsSingle();
        }
    }
}