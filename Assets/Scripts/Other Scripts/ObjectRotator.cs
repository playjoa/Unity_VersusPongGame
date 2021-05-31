using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [SerializeField]
    private bool rotateX = false, rotateY = false, rotateZ = true;

    [SerializeField]
    private float vel_Rotation = 300f;
    
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (rotateX)
            transform.Rotate(new Vector3(vel_Rotation * Time.deltaTime, 0, 0), Space.Self);

        if (rotateY)
            transform.Rotate(new Vector3(0, vel_Rotation * Time.deltaTime, 0), Space.Self);

        if (rotateZ)
            transform.Rotate(new Vector3(0, 0, vel_Rotation * Time.deltaTime), Space.Self);
    }
}