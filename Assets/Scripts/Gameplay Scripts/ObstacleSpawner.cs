using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform obstaclesParent;

    [SerializeField]
    private List<GameObject> possibleObstacles;

    [SerializeField]
    private RandomPosition2D spawnAreaPlayer_0, spawnAreaPlayer_1;

    private void Start()
    {
        InitializeObstacles();
    }

    void InitializeObstacles() 
    {
        if (possibleObstacles.Count == 0)
            return;

        if (spawnAreaPlayer_0 == null || spawnAreaPlayer_1 == null)
            return;

        SpawnObstacleInArea(spawnAreaPlayer_0.SpawnPosition());
        SpawnObstacleInArea(spawnAreaPlayer_1.SpawnPosition());
    }

    void SpawnObstacleInArea(Vector2 spawnPosition)
    {
       GameObject tempObstacle = Instantiate(RandomObstacle(), spawnPosition, Quaternion.identity);

        SetParent(tempObstacle);
    }

    void SetParent(GameObject objectToSetParent)
    {
        if (obstaclesParent != null)
            objectToSetParent.transform.SetParent(obstaclesParent);
    }

    GameObject RandomObstacle()
    {
        int idRandomObjstacle = Random.Range(0, possibleObstacles.Count);

        return possibleObstacles[idRandomObjstacle];
    }
}
