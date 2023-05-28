using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public float spawnInterval = 2f;
    public float zRange = 30f;
    public float xRange = 25f;

    private bool spawningEnabled = true;
    private bool bucketCheck = false;

    private void Start()
    {
        spawningEnabled = true;
        StartSpawning();
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnFish());
    }
    int checkScore()
    {
        if (ScoreManager.instance.getScore() > 50)
        {
            return 0;
        }
        return 1;
    }
    IEnumerator SpawnFish()
    {
        while (spawningEnabled)
        {
            // Choose a random fish prefab
            int fishIndex = Random.Range(0, fishPrefabs.Length-checkScore());
            GameObject fishPrefab = fishPrefabs[fishIndex];

            // Choose a random spawn position
            float xPos = Random.Range(-xRange, xRange);
            float zPos = Random.Range(-zRange, zRange);
            Vector3 spawnPos = new Vector3(xPos, 2f, zPos);

            // Spawn the fish
            Instantiate(fishPrefab, spawnPos, fishPrefab.transform.rotation);
            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
