using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongAnimation : MonoBehaviour
{
    [SerializeField]
    private float baseSize = 1;

    private void OnDisable()
    {
        transform.localScale = Vector3.one * baseSize;
    }

    void FixedUpdate()
    {
        Animar();
    }

    void Animar()
    {
        if (LeanTween.isTweening(gameObject))
            return;

        float anim = baseSize + Mathf.Sin(Time.time * 5.5f) * baseSize / 30f;
        transform.localScale = Vector3.one * anim;
    }
}
