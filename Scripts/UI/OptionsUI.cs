using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    // Audio and SFX content
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;

    // Keyboard Keys
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button resetKeyBindButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    // Gamepad Keys
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    // Mobile Keys
    [SerializeField] private TextMeshProUGUI mobileInteractText;
    [SerializeField] private TextMeshProUGUI mobileInteractAlternateText;


    [SerializeField] private Transform pressToRebindKeyTransform;

    // Platform dependent UI elements
    [SerializeField] private GameObject windowsKeybindContent;      // Windows Key Rebind option : windows execlusive UI
    [SerializeField] private GameObject gamepadKeybindContent;      // Windows Key Rebind option : windows execlusive UI
    [SerializeField] private GameObject mobileControllerContent;       // mobile controller option : android execlusive UI
    [SerializeField] private GameObject mobileControllerUI;     // mobile controller UI : android execlusive UI

    private Action onCloseButtonAction;

    private void Awake() {
        Instance = this;

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

        soundEffectsSlider.onValueChanged.AddListener(OnSoundEffectsVolumeChanged);

        closeButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });
        
        moveUpButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        
        moveDownButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        
        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        
        moveRightButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact);
        });
        
        interactAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });
        
        pauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Pause);
        });

        gamepadInteractButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });

        gamepadInteractAlternateButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });
        
        gamepadPauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });

        resetKeyBindButton.onClick.AddListener(() => {
            ResetToDefaultBindings();
        });
    }

    private void Start() {
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;

        UpdateVisual();

        ConfigureOptionsUIForPlatform();

        Hide();
        HidePressToRebindKey();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    // Shows UI content execlusive to that platform
    private void ConfigureOptionsUIForPlatform() {
        // Detects platform from PlatformUIManager
        bool isMobile = PlatformUIManager.Instance != null && PlatformUIManager.Instance.IsMobile;
        
        // Shows Windows Keybind Content
        if (windowsKeybindContent != null) {
            windowsKeybindContent.SetActive(!isMobile);
        }

        // Shows Gamepad Keybind Content
        if (gamepadKeybindContent != null) {
            gamepadKeybindContent.SetActive(!isMobile);
        }
        
        // Shows Mobile Controller Content
        if (mobileControllerContent != null) {
            mobileControllerContent.SetActive(isMobile);
        }
        
        // Shows Mobile Controller UI
        if (mobileControllerUI != null) {
            mobileControllerUI.SetActive(isMobile);
        }

    }

    public void UpdateVisual() {
        
        // Shows the keybind text
        // Keyboard Keys
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        if (GameInput.Instance.GetBindingText(GameInput.Binding.Pause) == "Escape") {
            pauseText.text = "ESC";
        } else {
            pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        }

        // Gamepad Keys
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

        // Mobile Keys
        mobileInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        mobileInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsSlider.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    //  Set music volume when slider value changes
    private void OnMusicVolumeChanged(float sliderValue) {
        MusicManager.Instance.SetMusicVolume(sliderValue);
    }

    //  Set sound effects volume when slider value changes
    private void OnSoundEffectsVolumeChanged(float sliderValue) {
        SoundManager.Instance.SetAudioVolume(sliderValue);
    }

    private void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

    //  Resets all the key binds to default
    private void ResetToDefaultBindings() {
        GameInput.Instance.ResetBindingsToDefault();
        
        UpdateVisual();
    }

}
