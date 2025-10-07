using AssetManagement.AssetProviders;
using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using AssetManagement.Scenes;
using Bootstrap;
using Gameplay.Configs;
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
            Container.Bind<AssetProvidersController>().AsSingle();
            
            Container.Bind<IAssetLoader>().To<AssetLoader>().AsSingle();

            Container.Bind(typeof(IAssetProvider), typeof(BaseConfigProvider<GameplayConfig>))
                .To<GameplayConfigsProvider>()
                .AsSingle();
            
            Container.Bind(typeof(IAssetProvider), typeof(BaseConfigProvider<UIPopupsConfig>))
                .To<UIPopupsConfigProvider>()
                .AsSingle();
            
            Container.Bind(typeof(IAssetProvider), typeof(UIPrefabsProvider))
                .To<UIPrefabsProvider>()
                .AsSingle();
        }
    }
}