using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float inputX;
    float inputY;
    public float speed = 5;
    Vector3 moveDirection;
    Vector2 facingDir;
    bool gunLoaded = true;
    bool piercingShotEnabled;
    CameraController camController;
    [SerializeField] Transform aim;
    [SerializeField] Camera cam;
    [SerializeField] Transform bulletPrefab;
    [SerializeField] int health = 50;
    [SerializeField] float iFrames = 3;
    [SerializeField] bool isInvincible = false;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinkRate = 0.1f;
    [SerializeField] AudioClip powerUpClip;
    [SerializeField] float fireRate = 1;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            UIManager.Instance.UpdateUIHealth(health);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        camController = FindObjectOfType<CameraController>();
        UIManager.Instance.UpdateUIHealth(health);
        UIManager.Instance.UpdateUIScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        readInput();
        // Player Movement
        transform.position += moveDirection * Time.deltaTime * speed;

        //Aim Movement
        facingDir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aim.position = transform.position + (Vector3)facingDir.normalized;
 
        shootGun();
        updatePlayerGraphics();

    }

    public void readInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        moveDirection.x = inputX;
        moveDirection.y = inputY;
        
    }

    public void shootGun()
    {
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Transform bulletClone = Instantiate(bulletPrefab, transform.position, targetRotation);
            if (piercingShotEnabled)
            {
                bulletClone.GetComponent<Bullet>().piercingShot = true;
            }
            StartCoroutine(ReloadGun());
        }
    }

    public void updatePlayerGraphics()
    {
        anim.SetFloat("Speed", moveDirection.magnitude);
        if (aim.position.x > transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else if (aim.position.x < transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
    }

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1/fireRate);
        gunLoaded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Receive Damage
        if (collision.CompareTag("Enemy"))
        {
            fireRate = 1;
            piercingShotEnabled = false;
           
            if (!isInvincible)
            {
                //Damage Taken
                Health -= collision.GetComponent<Enemy>().dmg;
                camController.Shake();
                //Death Check
                if (health <= 0)
                {
                    Destroy(gameObject);
                    UIManager.Instance.ShowGameOverScreen();
                }
                //Iframes
                StartCoroutine(invincivilityFramesRoutine(iFrames));

            }
            return;
        }

        //Catch PowerUp
        if (collision.CompareTag("PowerUp"))
        {
            switch (collision.GetComponent<PowerUp>().pUpType)
            {
                case PowerUp.powerUpType.FireRateIncrease:
                    //Increase Fire Rate
                    fireRate++;
                    break;

                case PowerUp.powerUpType.PiercingShot:
                    //Bullets Pierce Enemies
                    piercingShotEnabled = true;
                    break;
                    
            }
            Destroy(collision.gameObject, 0.1f);
            AudioSource.PlayClipAtPoint(powerUpClip, transform.position);
        }
    }


    IEnumerator invincivilityFramesRoutine(float iframes)
    {
        StartCoroutine(BlinkRoutine());
        isInvincible = true;
        yield return new WaitForSeconds(iframes);
        isInvincible = false;
    }

    IEnumerator BlinkRoutine()
    {
        int blinkTimes = 10;
        while(blinkTimes > 0)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkTimes * blinkRate);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkTimes * blinkRate);
            blinkTimes--;
        }
    }
}
