using UnityEngine;

public class AudioOnObjectEnable : MonoBehaviour
{
    [SerializeField]
    private string audioID = "bulletfire";

    [Range(0f, 1f)]
    [SerializeField]
    private float audioVolume = 1;

    private void OnEnable()
    {
        RequestAudioToPlay();
    }

    void RequestAudioToPlay() 
    {
        SoundManager.PlaySoundInList(audioID, audioVolume);
    }
}