using System;
using UnityEngine;

public class HealthBonusAward : BonusAward
{
    [SerializeField]
    private int healthAmmountToheal = 35;

    [SerializeField]
    private Color txtHealPopUpColor = Color.green;

    public static Action<int, int> HealPlayer;

    private const string textDamageID = "damageText";

    public override void AwardPlayer(BulletStats bulletHit)
    {
        base.AwardPlayer(bulletHit);

        HealPlayer(bulletHit.PlayerOwnerID, healthAmmountToheal);
        SpawnDamageText();
        DestroyBonus();
    }


    void SpawnDamageText()
    {
        DamageText damageTextToSet = ObjectPooler.Instance.RequestObject(textDamageID, transform.position).GetComponent<DamageText>();

        if (damageTextToSet == null)
            return;

        damageTextToSet.SetDamageText(HealthAmmountGiven, txtHealPopUpColor);
    }

    string HealthAmmountGiven => "+" + healthAmmountToheal;
}