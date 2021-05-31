using System;
using UnityEngine;

public abstract class BonusAward : MonoBehaviour
{
    [SerializeField]
    private string bonusExplosionID = "greenExplo";

    public virtual void AwardPlayer(BulletStats bulletHit) { }

    protected void DestroyBonus()
    {
        ObjectPooler.Instance.RequestObject(bonusExplosionID, transform.position);
        gameObject.SetActive(false);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            BulletStats bulletHit = collision.gameObject.GetComponent<BulletStats>();

            AwardPlayer(bulletHit);
        }
    }
}