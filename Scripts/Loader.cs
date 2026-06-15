using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        LoadingScene,
        MainMenuScene,
    }

    private static string targetSceneName;

    // Load level with Scene name
    public static void Load(Scene targetScene) {
        Loader.targetSceneName = targetScene.ToString();
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    // Load level with level number
    public static void LoadLevel(int levelNumber) {
        targetSceneName = $"Level_{levelNumber}";
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetSceneName);
    }
}
