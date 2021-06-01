using UnityEngine;
using PlayerLogic;
using PlayerLogic.Actions;

public class BulletDamage : MonoBehaviour
{
    [SerializeField]
    private int _bulletDamageAmmount = 15;

    private BulletStats bulletStats;

    public int BulletBaseDamage => _bulletDamageAmmount;
    public int BulletCurrentDamage => currentBulletDamage;

    private int currentBulletDamage;

    private void OnEnable()
    {
        GetBulletValues();
    }

    void GetBulletValues()
    {
        if (bulletStats == null)
            bulletStats = GetComponent<BulletStats>();

        currentBulletDamage = _bulletDamageAmmount;
    }

    public void SetNewDamageToBullet(int newBulletDamageToSet) 
    {
        currentBulletDamage = newBulletDamageToSet;
    }

    void DamagePlayer(PlayerHealth playerToDamage) 
    {
        if (playerToDamage == null)
            return;

        playerToDamage.TakeDamage(currentBulletDamage);
        SpawnDamageText();
    }

    void SpawnDamageText()
    {
        RequestDamageText.RequestText(DamageGivenInString, bulletStats.BulletColor, transform.position);
    }

    string DamageGivenInString => "-" + currentBulletDamage;

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