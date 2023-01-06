using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] int addedTime = 10;
    [SerializeField] AudioClip powerUpClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.time += addedTime;
            Destroy(gameObject,0.1f);
            AudioSource.PlayClipAtPoint(powerUpClip, transform.position);
        }
    }
}
