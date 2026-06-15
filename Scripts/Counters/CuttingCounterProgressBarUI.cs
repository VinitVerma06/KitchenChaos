using UnityEngine;
using UnityEngine.UI;

public class CuttingCounterProgressBarUI : MonoBehaviour {

    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;


    private void Start() {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        barImage.fillAmount = 0f;

        Hide();
    }


    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
        if (e != null) {
            barImage.fillAmount = e.progressNormalized;
            if (e.progressNormalized == 1f) {     // Hide the progress bar when it's full
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
