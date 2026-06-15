using UnityEngine;

public class BellCounterSound : MonoBehaviour {
    
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        BellCounter.Instance.OnBellInteract += BellCounter_OnBellInteract;
    }

    private void BellCounter_OnBellInteract(object sender, System.EventArgs e) {
        audioSource.Play();
    }
}
