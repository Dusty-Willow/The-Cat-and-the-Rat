using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public float gravity = -5;
    public Vector2 velocity;
    public float playerSpeed = 2;
    public float jumpVelocity = 2;
    public float groundHeight = (float) -0.3;
    public bool isGrounded = false;

    private Animator myAnimator;


    public Text mySushiScore;
    public float mySushi;
    public Text mySalmonScore;
    public float mySalmon;
    public Text myHealthScore;
    public float myHealth;

    private bool isInvincible, sushiCollected, salmonCollected;

    private GameObject player;
    private SpriteRenderer playerSprite;

    private GameObject rat;
    private Animator ratAnimator;
    private SpriteRenderer ratSprite;

    private GameObject myCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSprite = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        isInvincible = false;
        sushiCollected = false;
        salmonCollected = false;

        rat = GameObject.FindGameObjectWithTag("Rat");
        ratSprite = rat.GetComponent<SpriteRenderer>();
        ratAnimator = rat.GetComponent<Animator>();

        myCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            myAnimator.SetTrigger("Running");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
            }
        }

         if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            velocity.x = playerSpeed;
        }
        
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            velocity.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            velocity.x = -1 * playerSpeed;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            velocity.x = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;
            myAnimator.SetTrigger("Jumping");

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }
        pos.x += velocity.x;
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Sushi"))
        {
            StartCoroutine(CollectSushi());
        }
        else if (other.gameObject.CompareTag("Salmon"))
        {
            StartCoroutine(CollectSalmon());
        }
        else if (other.gameObject.CompareTag("Projectile") && !isInvincible)
        {
            StartCoroutine(InvulnerabilityProjectile());
        }
        else if (other.gameObject.CompareTag("Rat") && !isInvincible)
        {
            StartCoroutine(InvulnerabilityRat());
        }
        else if (other.gameObject.CompareTag("Border"))
        {
            velocity.x = 0;
        }
    }

    IEnumerator CollectSushi() 
    {
        sushiCollected = true;
        IncrementSushi();
        myCamera.GetComponent<ScreenShake>().TriggerShake();

        yield return new WaitForSeconds(1.4f);
        sushiCollected = false;
    }

    IEnumerator CollectSalmon() 
    {
        salmonCollected = true;
        IncrementSalmon();
        myCamera.GetComponent<ScreenShake>().TriggerShake();

        yield return new WaitForSeconds(1.4f);
        salmonCollected = false;
    }

    IEnumerator InvulnerabilityProjectile() 
    {
        if (myHealth == 1)
        {
            Destroy(player.gameObject);
        }
        isInvincible = true;
        DecrementHealth();
        playerSprite.color = new Color (1f,1f,1f,0.5f);
        Debug.Log("Invincibility Active!");

        yield return new WaitForSeconds(1.4f);
        isInvincible = false;
        playerSprite.color = new Color (1f,1f,1f,1f);
        Debug.Log("Invincibility Inactive!");
    }

    IEnumerator InvulnerabilityRat() 
    {
        if (myHealth == 1)
        {
            Destroy(player.gameObject);
        }
        isInvincible = true;
        ratAnimator.SetTrigger("Attacking");
        DecrementHealth();
        playerSprite.color = new Color (1f,1f,1f,0.5f);
        Debug.Log("Invincibility Active!");

        yield return new WaitForSeconds(1.4f);
        isInvincible = false;
        ratAnimator.SetTrigger("Running");
        playerSprite.color = new Color (1f,1f,1f,1f);
        Debug.Log("Invincibility Inactive!");
    }


    public void IncrementSushi()
    {
        
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            mySushi++;
            mySushiScore.text = ((int)mySushi).ToString() + " Sushi Collected!"; 
        }
    }

    public void IncrementSalmon()
    {
        
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            if (mySalmon < 2)
            {
                mySalmon++;
            }
            else
            {
                mySalmon = 0;
                if (myHealth < 5)
                {
                    myHealth++;
                }
                StartCoroutine(DamageRat());
            }
            myHealthScore.text = "Health: " + ((int)myHealth).ToString();
            mySalmonScore.text = ((int)mySalmon).ToString() + "/3 Salmon Collected!"; 
        }
    }

    IEnumerator DamageRat() 
    {
        ratSprite.color = new Color (1f,1f,1f,0.5f);
        ratAnimator.SetTrigger("Flinching");
        rat.GetComponent<ParticleSystem>().Play();
        rat.GetComponent<AudioSource>().Play();
        Debug.Log("Rat Damaged!");

        yield return new WaitForSeconds(1.3f);
        ratSprite.color = new Color (1f,1f,1f,1f);
        ratAnimator.SetTrigger("Running");
        rat.GetComponent<ParticleSystem>().Stop();
        rat.GetComponent<AudioSource>().Stop();
        Debug.Log("Invincibility Inactive!");
    }

    public void DecrementHealth()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null)
        {
            myHealth--;
            myHealthScore.text = "Health: " + ((int)myHealth).ToString();
        }
    }
}
