using System.Collections.Generic;
using System.Threading.Tasks;
using AssetManagement;
using AssetManagement.AssetProviders;
using AssetManagement.AssetProviders.Core;
using AssetManagement.Configs;
using AssetManagement.Scenes;
using Constants;
using Cysharp.Threading.Tasks;

namespace StateMachine.App
{
    public class GameplayAppState : BaseAppState
    {
        private readonly SceneTransitionController _sceneTransitionController;

        private List<IAssetProvider> _assetProviders = new();
        
        public GameplayAppState(BaseConfigProvider<UIPopupsConfig> uiPopupsConfigProvider, UIPrefabsProvider uiPrefabsProvider,
            SceneTransitionController sceneTransitionController)
        {
            _sceneTransitionController = sceneTransitionController;
            
            _assetProviders.Add(uiPrefabsProvider);
            _assetProviders.Add(uiPopupsConfigProvider);
        }
        
        public override async UniTask EnterState()
        {
            await InitializeClasses();
            await _sceneTransitionController.LoadNextScene(ConstSceneNames.GameplayScene);
        }

        public override async UniTask ExitState()
        {
            foreach (var disposable in _assetProviders) 
                disposable.Dispose();
            
            await _sceneTransitionController.LoadNextScene(ConstSceneNames.MainMenuScene);
        }

        private async Task InitializeClasses()
        {
            UniTask[] initializeTasks = new UniTask[_assetProviders.Count];

            for (int i = 0; i < _assetProviders.Count; i++) 
                initializeTasks[i] = _assetProviders[i].Initialize();
            
            await UniTask.WhenAll(initializeTasks);
        }
    }
}