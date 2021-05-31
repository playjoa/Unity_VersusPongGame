using System.Collections.Generic;
using UnityEngine;

public class MenuBulletMovement : BouncyObject
{
    [SerializeField]
    private int maxBounces = 20;

    [SerializeField]
    private float minSpeed = 8f, maxSpeed = 12f;

    [SerializeField]
    private string bulletExploID = "bulletexplo";

    private List<string> bounceableObjects = new List<string> { "bounceSurface", "bullet" };
    private int currentBounces;

    private void Awake()
    {
        SetBounceableObjects();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        StartBulletDirection();
    }

    public void StartBulletDirection()
    {
        SetVelocity(Random.Range(minSpeed, maxSpeed));
        SetObjectDirection(RandomDirection());

        currentBounces = maxBounces;
    }

    public override void OnObjectBounce()
    {
        CountBounce();
    }

    public override void MoveObject()
    {
        transform.Translate(objectMovementDirection * objectVelocity * Time.deltaTime);
    }

    void SetBounceableObjects()
    {
        SetNewListOfBounceables(bounceableObjects);
    }

    float DirectionRange => Random.Range(-1f, 1f);

    Vector2 RandomDirection()
    {
        return new Vector2(DirectionRange, DirectionRange);
    }

    void CountBounce()
    {
        currentBounces--;

        CheckIfOutOfBounces();
    }

    void DestroyBullet()
    {
        ObjectPooler.Instance.RequestObject(bulletExploID, transform.position);
        gameObject.SetActive(false);
    }

    void CheckIfOutOfBounces()
    {
        if (currentBounces < 0)
            DestroyBullet();
        else
            SoundManager.PlaySoundInList("bulletbounce", 0.2f);
    }
}