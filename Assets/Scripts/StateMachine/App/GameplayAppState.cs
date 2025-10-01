using AssetManagement;
using AssetManagement.AssetProviders;
using AssetManagement.Scenes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StateMachine.App
{
    public class GameplayAppState : BaseAppState
    {
        private readonly UIPrefabsProvider _uiPrefabsProvider;
        private readonly SceneTransitionController _sceneTransitionController;

        public GameplayAppState(UIPrefabsProvider uiPrefabsProvider, SceneTransitionController sceneTransitionController)
        {
            _uiPrefabsProvider = uiPrefabsProvider;
            _sceneTransitionController = sceneTransitionController;
        }
        
        public override async UniTask EnterState()
        {
            Debug.Log("Entered: Gameplay");
            await _uiPrefabsProvider.Initialize();
            await _sceneTransitionController.LoadNextScene(ConstSceneNames.GameplayScene);
        }

        public override async UniTask ExitState()
        {
            Debug.Log("Left: Gameplay");
            _uiPrefabsProvider.Dispose();
            await _sceneTransitionController.LoadNextScene(ConstSceneNames.MainMenuScene);
        }
    }
}