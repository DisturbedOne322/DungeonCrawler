using Gameplay.Combat;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SkillSelectionProvider>().To<PlayerSkillSelectionProvider>().AsSingle();
        }
    }
}