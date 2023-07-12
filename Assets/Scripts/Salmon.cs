using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salmon : MonoBehaviour
{
    public Vector3 speed;
    public float lifeTime = 1.3f;
    private Animator myAnimator;

    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start() {
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update() {
        transform.Translate(speed);
        if (gameObject.transform.position.x < -1)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        myAnimator.SetTrigger("Salmon Collected");
        speed -= speed;
        audioSource.pitch = 1.2f;
        audioSource.Play();

        Destroy(gameObject, lifeTime);
        Debug.Log("Salmon Collected!");
    }
}
