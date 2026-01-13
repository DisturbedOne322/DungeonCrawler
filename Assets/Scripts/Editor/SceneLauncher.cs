using Data.Constants;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class SceneLauncher
    {
        [MenuItem("Play/Play From Bootstrap Scene %#t")]
        public static void PlayFromBootstrapScene()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(AssetsPathsHelper.BootstrapScenePath);
                EditorApplication.isPlaying = true;
            }
        }

        [MenuItem("Play/Open Test Scene %#y")]
        public static void OpenGameplayScene() => EditorSceneManager.OpenScene(AssetsPathsHelper.GameplayScenePath);
    }
}