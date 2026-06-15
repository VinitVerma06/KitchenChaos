using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static event Action OnMenuClosed;

    [SerializeField] private CinemachineCamera mainMenuCamera;
    [SerializeField] private CinemachineCamera levelSelectCamera;

    private float blendTimeDuration = 1.5f;

    private void Start() {
        MainMenuUI.OnPlayButtonClicked += MainMenuUI_OnPlayButtonClicked;
    }

    private void MainMenuUI_OnPlayButtonClicked() {
        ShowLevelSelect();
    }

    private void ShowLevelSelect() {
        levelSelectCamera.Priority = 10;
        mainMenuCamera.Priority = 0;

        Invoke(nameof(ShowLevelSelectorUI), blendTimeDuration);
    }    
    
    private void HideLevelSelect() {
        levelSelectCamera.Priority = 0;
        mainMenuCamera.Priority = 10;

        OnMenuClosed?.Invoke();
    }

    private void ShowLevelSelectorUI() {
        LevelMenuUI.Instance.Show(HideLevelSelect);
    }

    private void OnDestroy() {
        MainMenuUI.OnPlayButtonClicked -= MainMenuUI_OnPlayButtonClicked;
    }
}
