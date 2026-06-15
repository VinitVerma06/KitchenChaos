using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelManager {

    private const string PLAYER_PREFS_UNLOCKED_LEVEL = "UnlockedLevel";

    public const int TOTAL_LEVELS = 3;

    // Unlock next level upon completion
    public static void CompleteLevel(int completedLevelNumber) {
        int currentUnlockedLevel = GetUnlockedLevel();

        if (completedLevelNumber >= currentUnlockedLevel) {
            int newUnlockedLevel = completedLevelNumber + 1;

            if (newUnlockedLevel > TOTAL_LEVELS) return;

            PlayerPrefs.SetInt(PLAYER_PREFS_UNLOCKED_LEVEL, newUnlockedLevel);
            PlayerPrefs.Save();
        }
    }

    public static int GetCurrentLevelNumber() {
        string sceneName = SceneManager.GetActiveScene().name;
        string levelNumberString = sceneName.Replace("Level", "").Replace("_", "").Replace(" ", "").Trim();
        
        if (int.TryParse(levelNumberString, out int levelNumber)) {
            return levelNumber;
        }

        return -1;
    }

    public static int GetUnlockedLevel() {
        return PlayerPrefs.GetInt(PLAYER_PREFS_UNLOCKED_LEVEL, 1);
    }

    public static bool IsLevelUnlocked(int levelNumber) {
        return levelNumber <= GetUnlockedLevel();
    }

    public static void ResetLevelProgression() {
        PlayerPrefs.SetInt(PLAYER_PREFS_UNLOCKED_LEVEL, 1);
    }
}