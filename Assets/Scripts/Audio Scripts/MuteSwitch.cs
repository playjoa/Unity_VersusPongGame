using UnityEngine;
using UnityEngine.UI;

public class MuteSwitch : MonoBehaviour {

    [SerializeField]
    private Image imgFXSound, imgMusic;

    [SerializeField]
    private Sprite[] sprFX, sprMusic;

    [Range(0f,1f)]
    [SerializeField]
    private float defaultMusicVolume = 0.75f;

    public static bool isFXMuted { get; private set; }
    public static bool isMusicMute { get; private set; }

    private void OnEnable()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        isFXMuted = PlayerPrefsBool.GetBool("fxMuted");
        isMusicMute = PlayerPrefsBool.GetBool("musicMuted");

        SpriteSwap();
        TurnDownMusicVolume();
    }

    void TurnDownMusicVolume()
    {
        GameObject musicSourceObject = GameObject.FindGameObjectWithTag("music");

        if (!musicSourceObject)
            return;

        AudioSource musicEmitter = musicSourceObject.GetComponent<AudioSource>();

        if (!musicEmitter)
            return;

        if (isMusicMute)
            musicEmitter.volume = 0;
        else
            musicEmitter.volume = defaultMusicVolume;
    }

    void SpriteSwap()
    {
        if (isFXMuted)
            imgFXSound.sprite = sprFX[1];
        else
            imgFXSound.sprite = sprFX[0];

        if (isMusicMute)
            imgMusic.sprite = sprMusic[1];
        else
            imgMusic.sprite = sprMusic[0];
    }

    public void MuteFX()
    {
        isFXMuted = !isFXMuted;

        PlayerPrefsBool.SetBool("fxMuted", isFXMuted);
        SpriteSwap();
    }

    public void MuteMusic()
    {
        isMusicMute = !isMusicMute;

        PlayerPrefsBool.SetBool("musicMuted", isMusicMute);
        SpriteSwap();
        TurnDownMusicVolume();
    }
}