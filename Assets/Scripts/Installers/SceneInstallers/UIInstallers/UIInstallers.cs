using Controllers;
using UI;
using Zenject;

namespace Installers.SceneInstallers.UIInstallers
{
    public class UIInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EquipmentChangeController>().AsSingle();
            Container.Bind<SkillDiscardController>().AsSingle();
            
            Container.Bind<UIFactory>().FromComponentInHierarchy().AsSingle();
        }
    }
}