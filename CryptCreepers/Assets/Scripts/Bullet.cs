using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 8;
    [SerializeField] float bulletLife = 5;
    [SerializeField] int health = 3;
    [SerializeField] Animator animator;
    public bool piercingShot;

    private void Start()
    {
        Destroy(gameObject, bulletLife);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage();

            if (!piercingShot)
            {
                Destroy(gameObject);
            }
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }

        }        
    }
}
