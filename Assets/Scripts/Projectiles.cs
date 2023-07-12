using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public Vector3 speed;
    public float lifeTime = 1.3f;
    public Explosion explosionPrefab; 
    private Animator myAnimator;

    private AudioSource audioSource;

    private bool hasEntered;


    // Start is called before the first frame update
    void Start() {
        // StartCoroutine(checkAlive()); 
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        hasEntered = false;

    }

    void Update() {
        transform.Translate(speed);
        if (gameObject.transform.position.x < -1)
        {
            Destroy(gameObject);
        }
    }

    // IEnumerator checkAlive() {
    //     // yield return new WaitForSeconds(lifeTime);
        
    //     // Destroy(gameObject);
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && !hasEntered)
        {
            hasEntered = true;
            myAnimator.SetTrigger("Colliding");
            speed -= speed;
            audioSource.Play();

            Destroy(gameObject, lifeTime);
            Debug.Log("Player hit!");
        }
    }
}
