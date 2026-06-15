using UnityEngine;

public class SoundManager : MonoBehaviour {

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private float audioVolume = 1f;
    private float maxAudioVolume = 10f;

    private void Awake() {
        Instance = this;

        //  Load PlayerPrefs
        audioVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnItemPicked += Player_OnItemPicked;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
        
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnItemPicked(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position) {
        AudioSource.PlayClipAtPoint(audioClip, position, audioVolume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume) {
        PlaySound(audioClipRefSO.footstep, position, volume);
    }

    public void SetAudioVolume(float volumeValue) {

        //  Convert slider value (0-10) to unity volume (0-1) 
        audioVolume = volumeValue / maxAudioVolume;

        //  Save to PlayerPrefs
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, audioVolume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return audioVolume;
    }

}
