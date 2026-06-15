using TMPro;
using UnityEngine;

public class StartDayTutorialUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI keyboardInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;

    private const string SHOW_TRIGGER = "Show";
    private const string HIDE_TRIGGER = "Hide";
    private const float fadeOutDuration = 0.25f;    // Tutorial_FadeOut clip duration

    private Animator animator;

    private void Awake() {
        if (animator == null) {
            animator = GetComponent<Animator>();
        }
    }

    private void Start() {
        UpdateVisual();
        Hide();

        TutorialUI.OnStartDayTutorialRequested += TutorialUI_OnStartDayTutorialRequested;
        TutorialUI.OnPlayerAltInteracted += TutorialUI_OnPlayerAltInteracted;
    }

    private void TutorialUI_OnStartDayTutorialRequested() {
        Show();
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void TutorialUI_OnPlayerAltInteracted() {
        animator.SetTrigger(HIDE_TRIGGER);
        Invoke(nameof(Hide), fadeOutDuration);
    }


    private void UpdateVisual() {
        keyboardInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        TutorialUI.OnStartDayTutorialRequested -= TutorialUI_OnStartDayTutorialRequested;
        TutorialUI.OnPlayerAltInteracted -= TutorialUI_OnPlayerAltInteracted;
    }
}
