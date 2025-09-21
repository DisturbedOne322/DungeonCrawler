using Data;
using Gameplay.Combat;
using Zenject;

namespace Installers.SceneInstallers
{
    public class GameDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CombatData>().AsSingle();
            Container.Bind<GameplayData>().AsSingle();
        }
    }
}