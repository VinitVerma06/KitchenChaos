using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour {

    [SerializeField] private Image playingTimerImage;

    private void Update() {
        playingTimerImage.fillAmount = GameHandler.Instance.GetPlayingTimerNormalized();
    }

}
