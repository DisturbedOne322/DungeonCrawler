using Gameplay.Combat;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SkillSelectionProvider>().To<AISkillSelectionProvider>().AsSingle();
        }
    }
}