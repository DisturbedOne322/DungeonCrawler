using Data;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Zenject;

namespace Installers.GameInstallers
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