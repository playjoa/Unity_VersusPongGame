using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [Range(1, 30)]
    [SerializeField]
    private float spawnRateInSeconds = 10;

    [Range(1f, 100f)]
    [SerializeField]
    private float spawnChancePercentage = 20;

    [Range(1, 5)]
    [SerializeField]
    private int maxBonusDuringGame = 3;

    [SerializeField]
    private List<string> bonusIDsToSpawn;

    [SerializeField]
    private RandomPosition2D bonusSpawnArea;

    private void Start()
    {
        InvokeRepeating("ProcessBonusSpawn", spawnRateInSeconds, spawnRateInSeconds);
    }

    void ProcessBonusSpawn()
    {
        if (!GameState.isPlaying())
        {
            CancelInvoke();
            return;
        }

        if (CanSpawn())
            SpawnBonus();
    }

    void SpawnBonus() 
    {
        if (bonusSpawnArea == null)
            return;

        ObjectPooler.Instance.RequestObject(BonusIDToSpawn(), bonusSpawnArea.SpawnPosition());
    }

    bool CanSpawn() 
    {
        if (TotalActiveBonuses() >= maxBonusDuringGame)
            return false;

        if (Random.Range(0f, 100f) <= spawnChancePercentage)
            return true;

        return false;
    }

    int TotalActiveBonuses() 
    {
        return FindObjectsOfType<BonusAward>().Length;
    }

    string BonusIDToSpawn()
    {
        if (bonusIDsToSpawn.Count == 0)
            return "";

        int idRandomBonusID = Random.Range(0, bonusIDsToSpawn.Count);

        return bonusIDsToSpawn[idRandomBonusID];
    }
}