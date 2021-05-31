using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGiver : MonoBehaviour
{
    [SerializeField]
    private int coinAmmountToGive = 100;

    private void Update()
    {
        if (isHoldingControl() && Input.GetKeyDown(KeyCode.F1))
            GiveMoneyToPlayer(0);

        if (isHoldingControl() && Input.GetKeyDown(KeyCode.F2))
            GiveMoneyToPlayer(1);
    }

    bool isHoldingControl()
    {
        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }

    public void GiveMoneyToPlayer(int idPlayer) 
    {
        Economy.AddCoinsToPlayer(idPlayer, coinAmmountToGive);
        SoundManager.PlaySoundInList("buy", 1);
    }
}