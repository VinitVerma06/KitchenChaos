using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {

    private const string NUMBERPOPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        GameHandler.Instance.OnGameStateChanged += GameHandler_OnGameStateChanged;

        Hide();
    }

    private void GameHandler_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameHandler.Instance.IsCountdownToStartActive()) {
            Show();
        } else {
            Hide();
        }

    }

    private void Update() {
        int countDownNumber = Mathf.CeilToInt(GameHandler.Instance.GetCountdownToStartTimer());
        countdownText.text = countDownNumber.ToString();

        if (previousCountdownNumber != countDownNumber) {
            previousCountdownNumber = countDownNumber;
            animator.SetTrigger(NUMBERPOPUP);
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
