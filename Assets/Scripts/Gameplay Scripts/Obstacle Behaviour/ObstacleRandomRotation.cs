using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRandomRotation : MonoBehaviour
{
    [SerializeField]
    private int[] possible_Z_Rotations = new int[] { 0, 90 };

    private void Start()
    {
        ToggleRandomRotation();
    }

    void ToggleRandomRotation()
    {
        SetRotationToNewOne(RandomRotationValue());
    }

    int RandomRotationValue()
    {
        return Random.Range(0, possible_Z_Rotations.Length);
    }

    void SetRotationToNewOne(int idRotation)
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, possible_Z_Rotations[idRotation]);
    }
}
