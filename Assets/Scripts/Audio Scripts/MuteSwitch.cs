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

    public static bool isFXMuted = false, isMusicMute = false;

    private void OnEnable()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        isFXMuted = GetBoolFromPlayerPrefs("fxMuted");
        isMusicMute = GetBoolFromPlayerPrefs("musicMuted");

        SpriteSwap();
        TurnDownMusicVolume();
    }

    void SetNewBoolValue(string id, bool valueToSet)
    {
        if (!valueToSet)
        {
            PlayerPrefs.SetInt(id, 0);
            return;
        }

        PlayerPrefs.SetInt(id, 1);
    }

    bool GetBoolFromPlayerPrefs(string id)
    {
        if (PlayerPrefs.GetInt(id) == 0)
            return false;

        return true;
    }

    void TurnDownMusicVolume()
    {
        try
        {
            GameObject music = GameObject.FindGameObjectWithTag("music");
            AudioSource musica = music.GetComponent<AudioSource>();

            if (isMusicMute)
                musica.volume = 0;
            else
                musica.volume = defaultMusicVolume;
        }
        catch { Debug.Log("Soundtrack not found!"); }
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

        SetNewBoolValue("fxMuted", isFXMuted);

        SpriteSwap();
    }

    public void MuteMusic()
    {
        isMusicMute = !isMusicMute;

        SetNewBoolValue("musicMuted", isMusicMute);

        SpriteSwap();

        TurnDownMusicVolume();
    }
}