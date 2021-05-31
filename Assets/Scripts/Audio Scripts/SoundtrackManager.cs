using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundtrackManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] playlistMenu, playlistGameplay;

    private AudioSource soundTrackSource;

    private void Awake()
    {
        CheckForOtherMusicSourcesAndDeleteThem();
        GetSoundTrackSourceIfNotSet();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void CheckForOtherMusicSourcesAndDeleteThem()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    void GetSoundTrackSourceIfNotSet()
    {
        if (soundTrackSource == null)
            soundTrackSource = GetComponent<AudioSource>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CancelInvoke();

        if (scene.name == "1_Menu")
            PlayRandomMusicFromMenuPlaylist();
        else
            PlayRandomMusicFromGameplayPlaylist();
    }

    void PlayRandomMusicFromMenuPlaylist()
    {
        soundTrackSource.clip = playlistMenu[RandomInSoundTrack(playlistMenu)];
        soundTrackSource.Play();
        Invoke("PlayRandomMusicFromMenuPlaylist", soundTrackSource.clip.length);
    }

    void PlayRandomMusicFromGameplayPlaylist()
    {
        soundTrackSource.clip = playlistGameplay[RandomInSoundTrack(playlistMenu)];
        soundTrackSource.Play();
        Invoke("PlayRandomMusicFromGameplayPlaylist", soundTrackSource.clip.length);
    }

    int RandomInSoundTrack(AudioClip[] playlistRef) => Random.Range(0, playlistRef.Length);
}