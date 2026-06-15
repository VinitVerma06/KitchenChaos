using UnityEngine;

public class PlatformUIManager : MonoBehaviour {

    public static PlatformUIManager Instance { get; private set; }

    [SerializeField] private bool simulateMobileInEditor = true;

    private bool isMobilePlatform = false;
    public bool IsMobile => isMobilePlatform;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        DetectDevicePlateform();
    }

    // Detect device platform
    private void DetectDevicePlateform() {
#if UNITY_EDITOR
        isMobilePlatform = simulateMobileInEditor;
        Debug.Log($"Editor Mode - Simulating Mobile: {isMobilePlatform}");

#elif UNITY_ANDROID
        isMobilePlatform = true;
        Debug.Log("Mobile platform detected");

#else
        isMobilePlatform = false;
        Debug.Log("Desktop platform detected");

#endif
    }
}