using AssetManagement.AssetProviders;
using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using AssetManagement.Scenes;
using Bootstrap;
using Gameplay.Progression;
using StateMachine.App;
using Zenject;

namespace Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProjectBootstrapper>().AsSingle();
            Container.Bind<SceneTransitionController>().AsSingle();

            BindAppStateMachine();
            BindAssetProviders();
        }

        private void BindAppStateMachine()
        {
            Container.Bind<AppStateMachine>().AsSingle();
            Container.Bind<BaseAppState>().To<MainMenuAppState>().AsSingle();
            Container.Bind<BaseAppState>().To<GameplayAppState>().AsSingle();
        }

        private void BindAssetProviders()
        {
            Container.Bind<IAssetLoader>().To<AssetLoader>().AsSingle();

            Container.Bind<UIPrefabsProvider>().AsSingle();
            Container.Bind<BaseConfigProvider<UIPopupsConfig>>().To<UIPopupsConfigProvider>().AsSingle();
            Container.Bind<BaseConfigProvider<GameplayConfig>>().To<GameplayConfigsProvider>().AsSingle();
        }
    }
}