using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerLogic;
using PlayerLogic.Actions;

public class BulletDamage : MonoBehaviour
{
    private BulletStats bulletStats;
    private const string textDamageID = "damageText";

    private void OnEnable()
    {
        GetBulletValues();
    }

    void GetBulletValues()
    {
        if (bulletStats == null)
            bulletStats = GetComponent<BulletStats>();
    }

    void DamagePlayer(PlayerHealth playerToDamage) 
    {
        if (playerToDamage == null)
            return;

        playerToDamage.TakeDamage(bulletStats.CurrentBulletDamage);
        SpawnDamageText();
    }

    void SpawnDamageText()
    {
        DamageText damageTextToSet = ObjectPooler.Instance.RequestObject(textDamageID, transform.position).GetComponent<DamageText>();

        if (damageTextToSet == null)
            return;

        damageTextToSet.SetDamageText(DamageGivenInString, bulletStats.BulletColor);
    }

    string DamageGivenInString => "-" + bulletStats.CurrentBulletDamage;

    void DestroyBullet() 
    {
        bulletStats.DestroyBullet();
    }

    bool IsThisAPlayerValidToTakeDamage(GameObject playerToCheck)
    {
        PlayerStats playerHit = playerToCheck.GetComponent<PlayerStats>();

        if (playerHit == null)
            return false;

        if (playerHit.PlayerID != bulletStats.PlayerOwnerID)
            return true;

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsThisAPlayerValidToTakeDamage(collision.gameObject))
        {
            PlayerHealth playerToDamage = collision.gameObject.GetComponent<PlayerHealth>();

            DamagePlayer(playerToDamage);
            DestroyBullet();
        }
    }
}
