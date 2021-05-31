using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    [System.Serializable]
    public class SoundFX
    {
        public string idSom = "click";
        public AudioClip[] clips;

        public AudioClip RandomSound() 
        {
            return clips[Random.Range(0, clips.Length)];
        }
    }

    [SerializeField]
    private List<SoundFX> sounds;

    [SerializeField]
    private AudioSource soundSource;

    static Dictionary<string, SoundFX> listaSons;
    static AudioSource storedNormalEmissor;

    private void Awake()
    {
        PassValuesToStaticSource();
        InitiateDictionary();
    }
    void PassValuesToStaticSource()
    {
        GetSoundSourceIfNotSet();
        storedNormalEmissor = soundSource;
    }

    void GetSoundSourceIfNotSet()
    {
        if (soundSource == null)
            soundSource = GetComponent<AudioSource>();
    }
    
    void InitiateDictionary()
    {
        listaSons = new Dictionary<string, SoundFX>();

        foreach (SoundFX s in sounds)
            listaSons.Add(s.idSom, s);
    }

    #region UI STUFF
    public void ClickUI()
    {
        PlaySoundInList("click", 1);
    }

    public void BackUI()
    {
        PlaySoundInList("back", 1);
    }

    public void PlayASound(string idSound)
    {
        PlaySoundInList(idSound, 1);
    }
    #endregion

    public static void PlaySoundInList(string id, float volume)
    {
        if (MuteSwitch.isFXMuted)
            return;

        if (!listaSons.ContainsKey(id))
        {
            Debug.LogWarning("No sound with this tag: " + id);
            return;
        }

        try
        {
            storedNormalEmissor.PlayOneShot(listaSons[id].RandomSound(), volume);
        }
        catch { }
    }
}