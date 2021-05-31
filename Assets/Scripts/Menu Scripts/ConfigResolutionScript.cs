using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigResolutionScript : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropDownResolutionOptions;

    private Resolution[] availableResolutions;

    private void OnEnable()
    {
        SetUpResolutionOptions();
    }

    void SetUpResolutionOptions()
    {
        List<string> resolutionNames = new List<string>();
        int idCurrentResolution = 0;

        availableResolutions = Screen.resolutions;
        dropDownResolutionOptions.ClearOptions(); 

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            resolutionNames.Add(ResolutionName(availableResolutions[i]));

            if (isResolutionCurrentResultion(availableResolutions[i]))
                idCurrentResolution = i;

        }

        dropDownResolutionOptions.AddOptions(resolutionNames);

        dropDownResolutionOptions.value = idCurrentResolution;
        dropDownResolutionOptions.RefreshShownValue();
    }

    bool isResolutionCurrentResultion(Resolution res)
    {
        if (res.width != Screen.currentResolution.width)
            return false;

        if (res.height != Screen.currentResolution.height)
            return false;

        if (res.refreshRate != Screen.currentResolution.refreshRate)
            return false;

        return true;
    }

    string ResolutionName(Resolution resolution)
    { 
        return resolution.width + " x " + resolution.height + " - " + resolution.refreshRate + " Hz"; 
    }

    public void SetResolutionAndRefreshRate(int idDesiredResolution)
    {
        Resolution resolutionToSet = availableResolutions[idDesiredResolution];
        
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = resolutionToSet.refreshRate;
        Screen.SetResolution(resolutionToSet.width, resolutionToSet.height, Screen.fullScreen); 
    }
}