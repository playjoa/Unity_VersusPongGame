using System;
using UnityEngine;

public class HealthBonusAward : BonusAward
{
    [SerializeField]
    private int healthAmmountToheal = 35;

    [SerializeField]
    private Color txtHealPopUpColor = Color.green;

    public static Action<int, int> HealPlayer;

    public override void AwardPlayer(BulletStats bulletHit)
    {
        base.AwardPlayer(bulletHit);

        HealPlayer(bulletHit.PlayerOwnerID, healthAmmountToheal);
        SpawnDamageText();
        DestroyBonus();
    }


    void SpawnDamageText()
    {
        RequestDamageText.RequestText(HealthAmmountGiven, txtHealPopUpColor, transform.position);
    }

    string HealthAmmountGiven => "+" + healthAmmountToheal;
}