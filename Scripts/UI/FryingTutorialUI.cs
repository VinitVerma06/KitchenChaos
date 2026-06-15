using TMPro;
using UnityEngine;

public class FryingTutorialUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI keyboardInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;

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

        TutorialUI.OnFryingTutorialRequested += TutorialUI_OnFryingTutorialRequested;
        TutorialUI.OnPlayerCooked += TutorialUI_OnPlayerCooked;
    }

    private void TutorialUI_OnFryingTutorialRequested() {
        Show();
        animator.SetTrigger(SHOW_TRIGGER);
    }

    private void TutorialUI_OnPlayerCooked() {
        animator.SetTrigger(HIDE_TRIGGER);
        Invoke(nameof(Hide), fadeOutDuration);
    }

    private void UpdateVisual() {
        keyboardInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {

    }
}
