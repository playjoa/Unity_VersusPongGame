using UnityEngine;
using UnityEngine.UI;

public class MenuStatusBar : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStatsUI 
    {
        [SerializeField]
        private Text txtVictoryCount;

        [SerializeField]
        private Text txtCoinsAmmount;

        public void SetVictoryCount(string textToSet)
        {
            if (!txtVictoryCount)
                return;

            txtVictoryCount.text = textToSet;
        }

        public void SetCoinsAmmount(string textToSet)
        {
            if (!txtCoinsAmmount)
                return;

            txtCoinsAmmount.text = textToSet;
        }
    }

    [SerializeField]
    private PlayerStatsUI[] playersStats;

    private void OnEnable()
    {
        Economy.OnCoinValueChange += UpdateStatusBar;
    }

    private void OnDisable()
    {
        Economy.OnCoinValueChange -= UpdateStatusBar;
    }

    private void Start()
    {
        UpdateStatusBar();
    }

    void UpdateStatusBar() 
    {
        for (int id = 0; id < playersStats.Length; id++)
        {
            playersStats[id].SetVictoryCount(":" + PlayersDataManager.PlayerVictoryCount(id));
            playersStats[id].SetCoinsAmmount(":" + Economy.ListTotalCoinsPlayer(id));
        }
    }
}