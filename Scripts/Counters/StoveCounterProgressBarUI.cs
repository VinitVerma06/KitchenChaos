using UnityEngine;
using UnityEngine.UI;

public class StoveCounterProgressBarUI : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private Image barImage;


    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        barImage.fillAmount = 0f;

        Hide();
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if (e.state == StoveCounter.State.Frying) {
            barImage.color = Color.green;
        }
        if (e.state == StoveCounter.State.Fried) {
            barImage.color = Color.red;
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, StoveCounter.OnProgressChangedEventArgs e) {
        if (e != null) {
            barImage.fillAmount = e.progressNormalized;
            if (e.progressNormalized == 1f || !stoveCounter.HasKitchenObject()) {     // Hide the progress bar when it's full or counter has no object
                Hide();
            } else {
                Show();
            }
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
