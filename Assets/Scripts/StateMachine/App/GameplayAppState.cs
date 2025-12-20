using AssetManagement.AssetProviders;
using AssetManagement.Scenes;
using Cysharp.Threading.Tasks;
using Data.Constants;

namespace StateMachine.App
{
    public class GameplayAppState : BaseAppState
    {
        private readonly AssetProvidersController _assetProvidersController;
        private readonly SceneTransitionController _sceneTransitionController;

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