using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundtrackManager : MonoBehaviour {

    [SerializeField]
    private AudioClip[] playlistMenu, playlistGameplay;

    public AudioSource soundTrackSource;

    private void Awake()
    {
        CheckForOtherMusicSourcesAndDeleteThem();

        GetSoundTrackSourceIfNotSet();
    }

    void GetSoundTrackSourceIfNotSet()
    {
        if (soundTrackSource == null)
            soundTrackSource = GetComponent<AudioSource>();
    }

    void CheckForOtherMusicSourcesAndDeleteThem() 
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        if (objs.Length > 1)
            Destroy(this.gameObject);
        
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    int RandomInSoundTrack(AudioClip[] playlistRef) => Random.Range(0, playlistRef.Length);

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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CancelInvoke();

        if (scene.name == "Menu")
            PlayRandomMusicFromMenuPlaylist();
        else
            PlayRandomMusicFromGameplayPlaylist();
    }
}