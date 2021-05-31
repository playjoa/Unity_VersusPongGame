using UnityEngine;

public class BulletBoostBonusAward : BonusAward
{
    [SerializeField]
    private float bulletBoostMultiplier = 1.5f;

    public override void AwardPlayer(BulletStats bulletHit)
    {
        BulletMovement bulletMovementRef = bulletHit.BulletMovement;

        if (bulletMovementRef == null)
            return;

        bulletMovementRef.BoostBulletVelocity(bulletBoostMultiplier);       
        DestroyBonus();
    }
}
