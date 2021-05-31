using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class BouncyObject : MonoBehaviour
{
    private protected Vector2 objectMovementDirection = Vector2.one.normalized;
    private protected float objectVelocity = 0;

    private Action OnBounce;
    private List<string> validBounceableSurfaces = new List<string> { "bounceSurface" };

    public virtual void OnEnable()
    {
        OnBounce += OnObjectBounce;
    }

    public virtual void OnDisable()
    {
        OnBounce -= OnObjectBounce;
    }

    public virtual void Update()
    {
        MoveObject();
    }

    public virtual void MoveObject()
    {
        if (GameState.isPaused())
            return;

        transform.Translate(objectMovementDirection * objectVelocity * Time.deltaTime);
    }

    protected void SetObjectDirection(Vector2 newDirection)
    {
        objectMovementDirection = newDirection.normalized;
    }

    protected void SetVelocity(float newVelocity)
    {
        objectVelocity = newVelocity;
    }

    protected void SetNewListOfBounceables(List<string> newBounceables)
    {
        validBounceableSurfaces = newBounceables;
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isThisAValidBounceableSurface(collision.gameObject))
        {
            BounceOnSurface(collision.contacts[0].normal);
        }
    }

    private bool isThisAValidBounceableSurface(GameObject objectHit)
    {
        foreach (string tagToCompare in validBounceableSurfaces)
        {
            if (objectHit.CompareTag(tagToCompare))
                return true;
        }

        return false;
    }

    protected void BounceOnSurface(Vector2 normalOfCollisionSurface)
    {
        Vector2 newBulletDirection = Vector2.Reflect(objectMovementDirection.normalized, normalOfCollisionSurface);
        objectMovementDirection = newBulletDirection;

        OnBounce?.Invoke();
    }

    public virtual void OnObjectBounce() { }
}