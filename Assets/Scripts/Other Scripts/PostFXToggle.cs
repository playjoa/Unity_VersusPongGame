using System;
using UnityEngine;
using UnityEngine.UI;

public class PostFXToggle : MonoBehaviour
{
    [System.Serializable]
    public class ToggleStats 
    {
        [SerializeField]
        private Color colorStatsToggle = Color.red;

        [SerializeField]
        private Sprite sprToggleStats;

        public void SetStatsToImage(Image imgToSet)
        {
            imgToSet.sprite = sprToggleStats;
            imgToSet.color = colorStatsToggle;
        }
    }

    [SerializeField]
    private Image imgToggle;

    [SerializeField]
    private ToggleStats OnToggleStats, OffToggleStats;

    private const string playerPostFXID = "postFXStats";

    public static Action OnPostFXValueChange;
    public static bool PostFXStats => PlayerPrefsBool.GetBool(playerPostFXID, true);

    private void OnEnable()
    {
        SetUpToggle();
    }

    void SetUpToggle()
    {
        SetUIChanges();
    }

    public void TogglePostFXValue() 
    {
        PlayerPrefsBool.SetBool(playerPostFXID, !PostFXStats);
        SetUIChanges();

        OnPostFXValueChange?.Invoke();
    }

    void SetUIChanges() 
    {
        if (PostFXStats)
        {
            OnToggleStats.SetStatsToImage(imgToggle);
            return;
        }
        OffToggleStats.SetStatsToImage(imgToggle);
    }
}