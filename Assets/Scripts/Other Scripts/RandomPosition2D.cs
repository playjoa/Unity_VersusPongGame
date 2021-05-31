using UnityEngine;

public class RandomPosition2D : MonoBehaviour
{
    [SerializeField]
    private Color GizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);

    private float xRange = 10f, yRange = 10f;

    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        xRange = transform.localScale.x / 2f;
        yRange = transform.localScale.y / 2f;
    }

    float Range(float x, float range)
    {
        return x + Random.Range(-range, range);
    }

    ///<summary>
    ///Will choose a random position inside this area (X, Y).
    ///</summary>
    public Vector3 SpawnPosition()
    {
        Vector3 posi;

        posi = new Vector3(Range(transform.position.x, xRange), Range(transform.position.y, yRange), 0);

        return posi;
    }
}
