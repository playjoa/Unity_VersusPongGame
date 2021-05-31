using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform obstaclesParent;

    [SerializeField]
    private List<ObstacleLevelData> obstacleLevelData;

    [SerializeField]
    private RandomPosition2D spawnAreaPlayer_0, spawnAreaPlayer_1;
    private int currentLevelToLoad => PlayersDataManager.CurrentLevelIDToPlay();
    
    private void Start()
    {
        InitializeObstacles();
    }

    void InitializeObstacles() 
    {
        if (obstacleLevelData.Count == 0)
            return;

        if (spawnAreaPlayer_0 == null || spawnAreaPlayer_1 == null)
            return;

        SpawnObstacleInArea(spawnAreaPlayer_0.RandomLocationInsideArea());
        SpawnObstacleInArea(spawnAreaPlayer_1.RandomLocationInsideArea());
    }

    void SpawnObstacleInArea(Vector2 spawnPosition)
    {
        obstacleLevelData[currentLevelToLoad].InstantiateRandomObstacleAtPosition(spawnPosition, obstaclesParent);
    }
}