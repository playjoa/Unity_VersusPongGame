using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Obstacles Level Data", fileName = "New Obstacles Data")]
public class ObstacleLevelData : ScriptableObject
{
    [Header("-----LevelID-----")]
    [SerializeField]
    private int _levelID = 0;

    [Header("-----List Of Possible Objects-----")]
    [SerializeField]
    private List<GameObject> _possibleObstacles;

    public int GetLevelID() 
    {
        return _levelID;
    }

    public void InstantiateRandomObstacleAtPosition(Vector3 positionToSpawn, Transform parentToSetObstacle) 
    {
        if (_possibleObstacles == null || _possibleObstacles.Count == 0)
            return;

        GameObject obstacleSpawned = Instantiate(RandomObstacle(), positionToSpawn, Quaternion.identity);
        SetParent(obstacleSpawned, parentToSetObstacle);
    }

    GameObject RandomObstacle()
    {
        int idRandomObjstacle = Random.Range(0, _possibleObstacles.Count);

        return _possibleObstacles[idRandomObjstacle];
    }

    void SetParent(GameObject objectToSetParent, Transform parentToSet)
    {
        if (parentToSet != null)
            objectToSetParent.transform.SetParent(parentToSet);
    }
}
