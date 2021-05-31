using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGiver : MonoBehaviour
{
    [SerializeField]
    private int coinAmmountToGive = 100;

    public void GiveMoneyToPlayer(int idPlayer) 
    {
        Economy.AddCoinsToPlayer(idPlayer, coinAmmountToGive);
    }
}