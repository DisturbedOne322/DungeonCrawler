using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public class SceneLauncher
    {
        private const string BootstrapScenePath = "Assets/Scenes/MainMenuScene.unity";
        private const string GameplayScenePath = "Assets/Scenes/GameplayScene.unity";

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
                EditorSceneManager.OpenScene(BootstrapScenePath);
                EditorApplication.isPlaying = true;
            }
        }

        [MenuItem("Play/Open Test Scene %#y")]
        public static void OpenGameplayScene() => EditorSceneManager.OpenScene(GameplayScenePath);
    }
}