using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private void LateUpdate() {
        // Makes progress bar face in the direction of camera
        transform.forward = Camera.main.transform.forward;
    }
}
