using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    [Header("-----Rotation Values-----")]
    [Range(5f, 300f)]
    [SerializeField]
    private float minRotationSpeed = 10f;

    [Range(5f, 300f)]
    [SerializeField]
    private float maxRotationSpeed = 250f;

    private float vel_Rotation;

    private void Start()
    {
        CalculateValues();
    }

    int RandomDirection()
    {
        if (Random.value >= 0.5f)
            return 1;

        return -1;
    }

    void CalculateValues()
    {
        vel_Rotation = Random.Range(minRotationSpeed, maxRotationSpeed);

        vel_Rotation *= RandomDirection();
    }

    void Update()
    {
        Rotate_Z_Axis();
    }

    void Rotate_Z_Axis()
    {
            transform.Rotate(new Vector3(0, 0, vel_Rotation * Time.deltaTime), Space.Self);
    }
}
