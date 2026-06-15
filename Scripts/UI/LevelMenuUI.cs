using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuUI : MonoBehaviour {

    public static LevelMenuUI Instance { get; private set; }

    public event Action OnCancelButtonClicked;

    [SerializeField] private Button cancelButton;
    [SerializeField] private Button[] levelButton;

    private void Awake() {
        Instance = this;

        cancelButton.onClick.AddListener(() => {
            Hide();
            OnCancelButtonClicked();
        });

        // For Testing : Reset level progress to 1
        //LevelManager.ResetLevelProgression();

        SetupLevelButtons();
    }

    private void Start() {
        Hide();
    }

    private void SetupLevelButtons() {
        // Disable all level buttons at load
        for (int i = 0; i < levelButton.Length; i++) {
            levelButton[i].interactable = false;
        }

        // Enable only unlocked and playable level
        for (int i = 0; i < levelButton.Length; i++) {
            int levelNumber = i + 1;

            if (levelNumber > LevelManager.TOTAL_LEVELS) break;

            bool isLevelUnlocked = LevelManager.IsLevelUnlocked(levelNumber);

            if (isLevelUnlocked) {
                levelButton[i].interactable = true;
                levelButton[i].onClick.AddListener(() => { Loader.LoadLevel(levelNumber); });
            }
        }
    }

    // Hide the main menu when level select menu is active and visa versa
    public void Show(Action OnCancelButtonClicked) {
        this.OnCancelButtonClicked = OnCancelButtonClicked;

        gameObject.SetActive(true);

        levelButton[0].Select();
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
