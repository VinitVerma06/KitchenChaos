using TMPro;
using UnityEngine;

public class EndDayTutorialUI : MonoBehaviour {

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

        TutorialUI.OnEndDayTutorialRequested += TutorialUI_OnEndDayTutorialRequested;
        TutorialUI.OnDayEnded += TutorialUI_OnDayEnded;
    }

    private void TutorialUI_OnEndDayTutorialRequested() {
        Show();
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void TutorialUI_OnDayEnded() {
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
        TutorialUI.OnEndDayTutorialRequested -= TutorialUI_OnEndDayTutorialRequested;
        TutorialUI.OnDayEnded -= TutorialUI_OnDayEnded;
    }
}
