using UnityEngine;

[RequireComponent(typeof(BulletMovement))]
[RequireComponent(typeof(BulletDamage))]
public class BulletStats : MonoBehaviour
{
    [SerializeField]
    private BulletMovement bulletMovement;

    [SerializeField]
    private BulletDamage bulletDamage;

    [SerializeField]
    private string bulletExploID = "bulletexplo";

    [SerializeField]
    private SpriteRenderer graphicsBullet;

    [SerializeField]
    private GameObject bulletBonusTrail;

    private Vector2 bulletStartDirection = Vector2.one;

    public int PlayerOwnerID { get; private set; }
    public int BaseBulletDamage => bulletDamage.BulletBaseDamage;
    public int CurrentBulletDamage => bulletDamage.BulletCurrentDamage;

    public Vector2 BulletStartDirection => bulletStartDirection;
    public BulletMovement BulletMovement => bulletMovement;
    public Color BulletColor => graphicsBullet.color;
    public Sprite sprBullet => graphicsBullet.sprite;
    public float BulletLocalScale => transform.localScale.x;

    private void Awake()
    {
        GetBulletComponentsIfNotSet();    
    }

    void GetBulletComponentsIfNotSet() 
    {
        if (!bulletMovement)
            bulletMovement.GetComponent<BulletMovement>();

        if (!bulletDamage)
            bulletDamage.GetComponent<BulletDamage>();
    }

    public void SetUpBullet(int ownerID, Vector2 startDirection, Color bulletColor)
    {
        PlayerOwnerID = ownerID;
        bulletStartDirection = startDirection.normalized;

        bulletMovement.StartBulletDirection();
        SetUpBulletColor(bulletColor);
    }

    public void DestroyBullet()
    {
        ObjectPooler.Instance.RequestObject(bulletExploID, transform.position);
        bulletBonusTrail.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SetNewDamage(int newDamageValue)
    {
        bulletDamage.SetNewDamageToBullet(newDamageValue);
        bulletBonusTrail.SetActive(true);
    }

    public void SetNewBulletSize(float scaleToSet)
    {
        if (scaleToSet < 0)
            return;

        transform.localScale = Vector3.one * scaleToSet;
    }

    void SetUpBulletColor(Color colorToSet)
    {
        if (graphicsBullet == null)
            return;

        graphicsBullet.color = colorToSet;
    }
}