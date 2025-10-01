using Cysharp.Threading.Tasks;
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
        private string _prevSceneName = null;
        
        public async UniTask LoadNextScene(string nextSceneName, bool unloadLoadingScene = true)
        {
            await LoadLoadingScene();
            await LoadScene(nextSceneName);
            await UnloadPrevScene();

            _prevSceneName = nextSceneName;

            if (unloadLoadingScene)
                await UnloadLoadingScene();
        }

        public async UniTask UnloadLoadingScene()
        {
            //LoadingViewController.Instance.SetProgress(1);
            //await LoadingViewController.Instance.FadeOut();
            
            await SceneManager.UnloadSceneAsync(ConstSceneNames.LoadingScene).ToUniTask();
        }

        private async UniTask LoadLoadingScene()
        {
            await Addressables.LoadSceneAsync(ConstSceneNames.LoadingScene, LoadSceneMode.Additive);
        }

        private async UniTask LoadScene(string nextSceneName)
        {
            _currentHandle = Addressables.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive, false);

            while (!_currentHandle.IsDone)
            {
                //LoadingViewController.Instance.SetProgress(_currentHandle.PercentComplete);
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
        
        private bool FirstSceneSwitch() => _prevSceneName is null;
    }
}