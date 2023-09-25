using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wB : MonoBehaviour
{
    public staminaBar staminarBarScript;
    float regen = 100f;
    public AudioSource soundplayer;
    
    private bool isCollided = false;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            
            isCollided = true;
            staminarBarScript.RegenStamina(regen);
            Disappear();
           soundplayer.Play();
        }
        Debug.Log("Collided");
    }

    void Disappear()
    {
        //move the bottle down
        Vector3 targetPosition = originalPosition - new Vector3(0f, 3f, 0f);
        transform.position = targetPosition;

        Invoke("ReappearObject", 10f);
    }

    void ReappearObject()
    {
        transform.position = originalPosition;
        isCollided = false;
    }
}
