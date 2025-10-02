using AssetManagement.AssetProviders;
using AssetManagement.Scenes;
using Constants;
using Cysharp.Threading.Tasks;

namespace StateMachine.App
{
    public class GameplayAppState : BaseAppState
    {
        private readonly SceneTransitionController _sceneTransitionController;
        private readonly AssetProvidersController _assetProvidersController;
        
        public GameplayAppState(
            SceneTransitionController sceneTransitionController,
            AssetProvidersController assetProvidersController)
        {
            _sceneTransitionController = sceneTransitionController;
            _assetProvidersController = assetProvidersController;
        }
        
        public override async UniTask EnterState()
        {
            await _assetProvidersController.Initialize();
            await _sceneTransitionController.LoadNextScene(ConstSceneNames.GameplayScene);
        }

        public override async UniTask ExitState()
        {
            _assetProvidersController.Dispose();
            
            await _sceneTransitionController.LoadNextScene(ConstSceneNames.MainMenuScene);
        }
    }
}