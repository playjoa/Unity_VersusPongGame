using System.Collections.Generic;
using UnityEngine;

public class MenuBulletSpawner : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField]
    private float frequencySpawnInSeconds = 3f;

    [SerializeField]
    private int maxBulletQty = 5;

    [SerializeField]
    private RandomPosition2D bulletSpawnArea;

    [SerializeField]
    private List<string> bulletMenuIDs;

    private void Start()
    {
        InvokeRepeating("ProcessSpawn", 0, frequencySpawnInSeconds);
    }

    private void OnDisable()
    {
        CancelInvoke();    
    }

    void ProcessSpawn()
    {
        if (CanSpawn())
            SpawnBullet();
    }

    void SpawnBullet()
    {
        if (bulletSpawnArea == null)
            return;

        ObjectPooler.Instance.RequestObject(BulletIDToSpawn(), bulletSpawnArea.SpawnPosition());
    }

    string BulletIDToSpawn()
    {
        if (bulletMenuIDs.Count == 0)
            return "";

        int idRandomBonusID = Random.Range(0, bulletMenuIDs.Count);

        return bulletMenuIDs[idRandomBonusID];
    }

    bool CanSpawn()
    {
        if (TotalActiveBonuses() >= maxBulletQty)
            return false;

        return true;
    }

    int TotalActiveBonuses()
    {
        return FindObjectsOfType<MenuBulletMovement>().Length;
    }
}