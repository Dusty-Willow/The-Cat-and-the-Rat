using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Transform myTransform;
    public float shakeDuration = 0f;
    public float shakeMagnitude = 0.05f;
    public float dampingSpeed = 1.0f;
    Vector2 initialPosition;

    void Awake()
    {
        if (myTransform == null)
        {
            myTransform = GetComponent(typeof(Transform)) as Transform;
        }        
    }

    void onEnable()
    {
        initialPosition = myTransform.localPosition;
    }

    void FixedUpdate()
    {
        if (shakeDuration > 0)
        {
        transform.localPosition = initialPosition + Random.insideUnitCircle * shakeMagnitude;
        
        shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
        shakeDuration = 0f;
        transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake() 
    {
        shakeDuration = 0.05f;
    }
}
