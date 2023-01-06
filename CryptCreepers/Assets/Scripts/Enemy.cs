using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;

    [SerializeField] float health = 10;
    [SerializeField] float speed = 1;
    [SerializeField] int scorePoints = 100;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip impactClip;
    [SerializeField] AudioClip enemyDeathClip;
    public int dmg = 5;
    private Player playerIframes;
    

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randSpawnPoint = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[randSpawnPoint].transform.position;
    }

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        //print("Ypos=" + direction.y + ", Xpos=" + direction.x);
        transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;      
    }

    public void TakeDamage()
    {
        health--;
        AudioSource.PlayClipAtPoint(impactClip, transform.position);
        if (health <= 0)
        {
            GameManager.instance.Score += scorePoints;
            Destroy(gameObject,0.1f);
            AudioSource.PlayClipAtPoint(enemyDeathClip, transform.position);
        }
    }

}

