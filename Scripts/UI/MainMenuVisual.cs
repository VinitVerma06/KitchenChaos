using UnityEngine;

public class MainMenuVisual : MonoBehaviour {


    [SerializeField] private GameObject mainMenu;

    private const string TRIGGER_SHOW = "Show";
    private const string TRIGGER_HIDE = "Hide";

    private Animator mainMenuAnimator;

    private float cameraBlendTimeDuration = 1.5f;

    private void Awake() {
        mainMenuAnimator = mainMenu.GetComponent<Animator>();
    }

    private void Start() {
        ShowAnimation();

        MainMenuUI.OnPlayButtonClicked += MainMenuUI_OnPlayButtonClicked;
        LevelMenuUI.Instance.OnCancelButtonClicked += LevelMenuUI_OnCancelButtonClicked;
    }

    private void LevelMenuUI_OnCancelButtonClicked() {
        Invoke(nameof(ShowAnimation), cameraBlendTimeDuration);
    }

    private void MainMenuUI_OnPlayButtonClicked() {
        HideAnimation();
    }

    private void ShowAnimation() {
        mainMenuAnimator.SetTrigger(TRIGGER_SHOW);
    }
    
    private void HideAnimation() {
        mainMenuAnimator.SetTrigger(TRIGGER_HIDE);
    }

    private void OnDestroy() {
        MainMenuUI.OnPlayButtonClicked -= MainMenuUI_OnPlayButtonClicked;
        LevelMenuUI.Instance.OnCancelButtonClicked -= LevelMenuUI_OnCancelButtonClicked;
    }
}
