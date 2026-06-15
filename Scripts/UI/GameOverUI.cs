using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        nextDayButton.onClick.AddListener(() => {
            Loader.LoadLevel(LevelManager.GetCurrentLevelNumber() + 1);
        });
    }

    private void Start() {
        GameHandler.Instance.OnGameStateChanged += GameHandler_OnGameStateChanged;

        SetupNextDayButton();

        Hide();
    }

    private void SetupNextDayButton() {
        
        // check if next level is available
        if (LevelManager.GetCurrentLevelNumber() >= LevelManager.TOTAL_LEVELS) {
            nextDayButton.interactable = false;
        } else {
            nextDayButton.interactable = true;
        }
    }

    private void GameHandler_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameHandler.Instance.IsGameOver()) {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipeDelivered().ToString();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);

        mainMenuButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        GameHandler.Instance.OnGameStateChanged -= GameHandler_OnGameStateChanged;
    }
}
