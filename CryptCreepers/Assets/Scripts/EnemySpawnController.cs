using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;
    [Range(0,10)][SerializeField] float spawnRate = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnNewEnemy());   
    }

    IEnumerator SpawnNewEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/spawnRate);
            float rnd = Random.Range(0.0f, 1.0f);

            if(rnd <= GameManager.instance.difficulty*0.3f)
            {
                Instantiate(enemyPrefab[0]);
            }
            else if ( rnd > GameManager.instance.difficulty * 0.3f && rnd <= GameManager.instance.difficulty * 0.6f)
            {
                Instantiate(enemyPrefab[1]);
            }
            else
            {
                Instantiate(enemyPrefab[2]);
            }
            
        }
    }
}
