using System.Collections.Generic;
using UnityEngine;

public class BonusMovement : BouncyObject
{
    [SerializeField]
    private float minSpeed = 1.5f, maxSpeed = 4f;

    private List<string> bounceableObjects = new List<string> { "bounceSurface", "boost", "Player" };

    private void Awake()
    {
        SetBounceableObjects();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        InitializeMovement();
    }
    void InitializeMovement()
    {
        SetVelocity(Random.Range(minSpeed, maxSpeed));
        SetObjectDirection(RandomDirection());
    }

    void SetBounceableObjects()
    {
        SetNewListOfBounceables(bounceableObjects);
    }

    Vector2 RandomDirection()
    {
        return new Vector2(DirectionRange, DirectionRange);
    }

    float DirectionRange => Random.Range(-1f, 1f);
}