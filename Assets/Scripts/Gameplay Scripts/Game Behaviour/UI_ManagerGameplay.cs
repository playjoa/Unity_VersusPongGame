using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerLogic.Actions;

public class UI_ManagerGameplay : MonoBehaviour
{
    [System.Serializable]
    public class UIPlayerHealth
    {
        [SerializeField]
        private Text txtPlayerHealthValue;

        [SerializeField]
        private Image imgFillPlayerHealth;

        public void SetFillPlayerHealthValue(float playerHealthPercentage)
        {
            if (imgFillPlayerHealth == null)
                return;

            imgFillPlayerHealth.fillAmount = playerHealthPercentage;
        }

        public void SetTextPlayerHealthValue(int valueToSet)
        {
            if (txtPlayerHealthValue == null)
                return;

            txtPlayerHealthValue.text = valueToSet.ToString();
        }
    }

    [SerializeField]
    private GameObject gamePlayScreen, pauseScreen, gameOverScreen, tutorialScreen;

    [SerializeField]
    private Text txtGameOverTitle, txtPlayer0Award, txtPlayer1Award, txtCountdownText;

    [SerializeField]
    private List<UIPlayerHealth> playersUIHealth;

    public static UI_ManagerGameplay Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdatePlayerHealthUI(PlayerHealth playerToUpdateUI) 
    {
        if (playersUIHealth.Count == 0)
            return;

        int idPlayer = playerToUpdateUI.PlayerID;

        if (playersUIHealth[idPlayer] == null)
            return;

        playersUIHealth[idPlayer].SetFillPlayerHealthValue(playerToUpdateUI.HealthPorcentage);
        playersUIHealth[idPlayer].SetTextPlayerHealthValue(playerToUpdateUI.CurrentPlayerHealth);
    }

    public void TogglePauseScreen(bool valueToSet)
    {
        pauseScreen.SetActive(valueToSet);
    }

    public void ToggleGamePlayScreen(bool valueToSet)
    {
        gamePlayScreen.SetActive(valueToSet);
    }

    public void ToggleGameOverScreen(bool valueToSet)
    {
        gameOverScreen.SetActive(valueToSet);
    }

    public void ToggleTutorialScreen(bool valueToSet)
    {
        tutorialScreen.SetActive(valueToSet);
    }

    public void ToggleAndSetCountdownText(string valueToSet)
    {
        if (txtCountdownText.gameObject.activeSelf)
            txtCountdownText.gameObject.SetActive(false);

        txtCountdownText.text = valueToSet;
        txtCountdownText.gameObject.SetActive(true);
    }

    public void SetGameOverTitleText(string textToSet)
    {
        txtGameOverTitle.text = textToSet;
    }

    public void SetPlayer0AwardText(string textToSet)
    {
        txtPlayer0Award.text = textToSet;
    }

    public void SetPlayer1AwardText(string textToSet)
    {
        txtPlayer1Award.text = textToSet;
    }
}