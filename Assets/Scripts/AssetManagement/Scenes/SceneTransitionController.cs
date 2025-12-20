using Cysharp.Threading.Tasks;
using Data.Constants;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace AssetManagement.Scenes
{
    public class SceneTransitionController
    {
        private AsyncOperationHandle<SceneInstance> _currentHandle;
        private string _prevSceneName;

        public async UniTask LoadNextScene(string nextSceneName, bool unloadLoadingScene = true)
        {
            await LoadLoadingScene();
            await UnloadPrevScene();
            await LoadScene(nextSceneName);

            _prevSceneName = nextSceneName;

            if (unloadLoadingScene)
                await UnloadLoadingScene();
        }

        public async UniTask UnloadLoadingScene()
        {
            await LoadingDisplay.Instance.FadeOut();
            await SceneManager.UnloadSceneAsync(ConstSceneNames.LoadingScene).ToUniTask();
        }

        private async UniTask LoadLoadingScene()
        {
            await Addressables.LoadSceneAsync(ConstSceneNames.LoadingScene, LoadSceneMode.Additive);
            await LoadingDisplay.Instance.FadeIn();
        }

        private async UniTask LoadScene(string nextSceneName)
        {
            _currentHandle = Addressables.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive, false);

            while (!_currentHandle.IsDone)
            {
                LoadingDisplay.Instance.SetProgress(_currentHandle.PercentComplete);
                await UniTask.NextFrame();
            }

            await _currentHandle.Result.ActivateAsync();
            SceneManager.SetActiveScene(_currentHandle.Result.Scene);
        }

        private async UniTask UnloadPrevScene()
        {
            if (FirstSceneSwitch())
                await SceneManager.UnloadSceneAsync(ConstSceneNames.MainMenuScene);
            else
                await SceneManager.UnloadSceneAsync(_prevSceneName);
        }

        private bool FirstSceneSwitch()
        {
            return _prevSceneName is null;
        }
    }
}