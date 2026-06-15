using TMPro;
using UnityEngine;

public class MovementTutorialUI : MonoBehaviour {

    [SerializeField] TextMeshProUGUI moveUpText;
    [SerializeField] TextMeshProUGUI moveLeftText;
    [SerializeField] TextMeshProUGUI moveDownText;
    [SerializeField] TextMeshProUGUI moveRightText;
    [SerializeField] TextMeshProUGUI moveGamepadText;

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

        TutorialUI.OnMovementTutorialRequested += TutorialUI_OnMovementTutorialRequested;
        TutorialUI.OnPlayerMoved += TutorialUI_OnPlayerMoved;
    }

    private void TutorialUI_OnMovementTutorialRequested() {
        Show();
        animator.SetTrigger(SHOW_TRIGGER);
    }
    private void TutorialUI_OnPlayerMoved() {
        animator.SetTrigger(HIDE_TRIGGER);
        Invoke(nameof(Hide), fadeOutDuration);
    }


    private void UpdateVisual() {
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        moveGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Move);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        TutorialUI.OnMovementTutorialRequested -= TutorialUI_OnMovementTutorialRequested;
        TutorialUI.OnPlayerMoved -= TutorialUI_OnPlayerMoved;
    }

}
