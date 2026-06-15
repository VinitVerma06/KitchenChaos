using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    public static event Action OnPlayButtonClicked;

    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            Hide();
            OnPlayButtonClicked?.Invoke();
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

    private void Start() {
        CameraController.OnMenuClosed += CameraController_OnMenuClosed;
    }

    private void CameraController_OnMenuClosed() {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);

        playButton.Select();    // Highlights the play button
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        CameraController.OnMenuClosed -= CameraController_OnMenuClosed;
    }
}
