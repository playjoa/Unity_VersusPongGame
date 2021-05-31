using UnityEngine;
using Cinemachine;
using PlayerLogic.Actions;

public class CameraShake : MonoBehaviour {
    
    [SerializeField]
    private CinemachineVirtualCamera VirtualCamera;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    
    private float _shakeAmplitude = 1.2f;         
    private float _shakeFrequency = 2.0f;       
    private float _shakeElapsedTime = 0f;

    private void OnEnable()
    {
        PlayerHealth.RequestCameraShake += Shake;
        PlayerShooter.RequestCameraShake += Shake;
    }

    private void OnDisable()
    {
        PlayerHealth.RequestCameraShake -= Shake;
        PlayerShooter.RequestCameraShake -= Shake;
    }

    void Start()
    {
        GetVirtualCameraNoise();
    }

    void Update()
    {
        ProcessShake();
    }

    void GetVirtualCameraNoise()
    {
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Shake(CameraShakeData shakeData)
    {
        _shakeElapsedTime = shakeData.Time;
        _shakeAmplitude = shakeData.Amplitude;
        _shakeFrequency = shakeData.Frequency;
    }

    void ProcessShake()
    {
        if (VirtualCamera == null || virtualCameraNoise == null)
            return;

        if (_shakeElapsedTime > 0)
        {
            virtualCameraNoise.m_AmplitudeGain = _shakeAmplitude;
            virtualCameraNoise.m_FrequencyGain = _shakeFrequency;

            _shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            virtualCameraNoise.m_AmplitudeGain = 0f;
            _shakeElapsedTime = 0f;
        }
    }
}