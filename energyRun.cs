using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energyRun : MonoBehaviour
{
    public AudioSource soundplayer;
    public Slider countdownSlider;
    public PlayerMovement playerMovementScript;
    float regen = 100f;
    public TrailRenderer trailRenderer;

    private bool isCollided = false;
    private Vector3 originalPosition;

    private float originalPlayerSpeed;
    private float effectDuration = 15f;
    private float timeLeft;

    private void Start()
    {
        originalPosition = transform.position;
        gameObject.SetActive(true);
        
        countdownSlider.maxValue = effectDuration;
        countdownSlider.value = effectDuration;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollided)
        {
            countdownSlider.gameObject.SetActive(true);
            isCollided = true;
            originalPlayerSpeed = playerMovementScript.playerSpeed;
            playerMovementScript.playerSpeed = 10.0f;
            timeLeft = effectDuration;
            StartCoroutine(CountdownToNormalSpeed());
            Disappear();
            trailRenderer.enabled = true;
            

            soundplayer.Play();
        }
    }

    private IEnumerator CountdownToNormalSpeed()
    {
        while (timeLeft > 0)
        {
            
            yield return new WaitForSeconds(Time.deltaTime);
            timeLeft -= Time.deltaTime;
            countdownSlider.value = timeLeft;
        }
        // After the effect duration, return player speed to normal
        playerMovementScript.playerSpeed = originalPlayerSpeed;
        isCollided = false;
        countdownSlider.gameObject.SetActive(false);
        trailRenderer.enabled = false;
    }

    void Disappear()
    {
        // Move the bottle down
        Vector3 targetPosition = originalPosition - new Vector3(0f, 3f, 0f);
        transform.position = targetPosition;

        Invoke("ReappearObject", 10f);
    }

    void ReappearObject()
    {
        transform.position = originalPosition;
    }
}
