using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        pauseButton.onClick.AddListener(() => {
            GameHandler.Instance.TogglePauseGame();
        });
        resumeButton.onClick.AddListener(() => {
            GameHandler.Instance.TogglePauseGame();
        });
        optionsButton.onClick.AddListener(() => {
            Hide();     // Hide GamePauseUI when opening OptionsUI

            OptionsUI.Instance.Show(Show);     // Show GamePauseUI when closing OptionsUI
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start() {
        GameHandler.Instance.OnGamePaused += GameHandler_OnGamePaused;
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;

        Hide();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void GameHandler_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
