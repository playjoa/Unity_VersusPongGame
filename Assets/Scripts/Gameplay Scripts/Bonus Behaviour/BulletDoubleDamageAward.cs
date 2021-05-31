using UnityEngine;

public class BulletDoubleDamageAward : BonusAward
{
    [SerializeField]
    private int bulletDamageMultiplier = 2;

    public override void AwardPlayer(BulletStats bulletHit)
    {
        int newDamageToApple = bulletHit.CurrentBulletDamage * bulletDamageMultiplier;

        bulletHit.SetNewDamage(newDamageToApple);
        DestroyBonus();
    }
}
