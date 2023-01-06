using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkPointPrefab;
    [SerializeField] float checkpointSpawnTimer = 5;
    [SerializeField] float powerUpSpawnTimer = 5;
    [SerializeField] int radius = 5;
    [SerializeField] GameObject[] powerUpPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnCheckpointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnCheckpointRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkpointSpawnTimer);
            Vector2 randomPosition = Random.insideUnitCircle*radius;
            Vector3 spawnPosition = checkPointPrefab.transform.position + (Vector3)randomPosition;
            Instantiate(checkPointPrefab, spawnPosition, Quaternion.identity);
        }

        
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnTimer);
            Vector2 randomPosition = Random.insideUnitCircle * radius;
            Vector3 spawnPosition = checkPointPrefab.transform.position + (Vector3)randomPosition;
            int random = Random.Range(0, powerUpPrefab.Length);
            Instantiate(powerUpPrefab[random], spawnPosition, Quaternion.identity);           
            
        }

    }
}
