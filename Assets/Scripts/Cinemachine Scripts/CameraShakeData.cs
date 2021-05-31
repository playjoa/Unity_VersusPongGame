using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Camera Shake Data", fileName = "New Shake Data")]
public class CameraShakeData : ScriptableObject
{
    [Header("-----Shake Data-----")]
    [SerializeField]
    private float _shakeAmplitude = 1.2f;

    [SerializeField]
    private float _shakeFrequency = 2.0f;

    [SerializeField]
    private float _shakeTime = 0.25f;

    public float Amplitude => _shakeAmplitude;
    public float Frequency => _shakeFrequency;
    public float Time => _shakeTime;
}