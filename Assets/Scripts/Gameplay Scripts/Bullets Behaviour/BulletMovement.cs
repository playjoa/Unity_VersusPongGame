using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletStats))]
public class BulletMovement : BouncyObject
{
    [SerializeField]
    private float _bulletVelocity = 12;

    [SerializeField]
    private int _maxBounces = 20;

    private BulletStats bulletStats;
    private int currentBounces;
    private List<string> bounceableObjects = new List<string> { "bounceSurface", "bullet" };

    public int BulletMaxBounces => _maxBounces;
    public float BulletVelocity => _bulletVelocity;

    private void Awake()
    {
        SetBounceableObjects();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        GetBulletValues(); 
        
        StartBulletDirection();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        ResetBullet();
    }

    void SetBounceableObjects()
    {
        SetNewListOfBounceables(bounceableObjects);
    }

    void GetBulletValues()
    {
        if (bulletStats == null)
            bulletStats = GetComponent<BulletStats>();
    }

    void ResetBullet()
    {
        SetVelocity(objectVelocity);
        SetObjectDirection(Vector2.zero);
        currentBounces = _maxBounces;
    }

    public void BoostBulletVelocity(float boostMultiplier)
    {
        float newBoostedVelocity = objectVelocity * boostMultiplier;

        SetVelocity(newBoostedVelocity);
    }

    public void StartBulletDirection()
    {
        SetObjectDirection(bulletStats.BulletStartDirection);
        SetVelocity(_bulletVelocity);

        currentBounces = _maxBounces;
    }

    void CountBounce()
    {
        currentBounces--;

        CheckIfOutOfBounces();
    }

    void CheckIfOutOfBounces()
    {
        if (currentBounces < 0)
        {
            bulletStats.DestroyBullet();
            return;
        }

        SoundManager.PlaySoundInList("bulletbounce", 0.35f);
    }

    public override void OnObjectBounce()
    {
        CountBounce();
    }
}