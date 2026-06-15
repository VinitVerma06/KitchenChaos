using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour {

    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    public static MusicManager Instance { get; private set; }

    private float musicVolume = 1f;
    private float maxMusicVolume = 10f;
    private AudioSource audioSource;

    private void Awake() {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        //  Load PlayerPrefs
        musicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        audioSource.volume = musicVolume;
    }

    public void SetMusicVolume(float volumeValue) {
        
        //  Convert slider value (0-10) to unity volume (0-1) 
        musicVolume = volumeValue / maxMusicVolume;

        audioSource.volume = musicVolume;

        //  Save to PlayerPrefs
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return musicVolume;
    }
}
