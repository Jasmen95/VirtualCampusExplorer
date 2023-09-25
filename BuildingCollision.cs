using MTAssets.EasyMinimapSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollision : MonoBehaviour
{
    public GameObject minimapItem;
    public Canvas canvas;
    public AudioSource soundplayer;

    public bool isEnabled = false; // Start with the script disabled.
    private bool isPlayerInside = false;

    private void DeactivateParticleHighlight()
    {
        MinimapItem minimapItemScript = minimapItem.GetComponent<MinimapItem>();
        minimapItemScript.particlesHighlightMode = MinimapItem.ParticlesHighlightMode.Disabled;
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isEnabled = false; // Deactivate the script after the delay.
        canvas.gameObject.SetActive(false);
    }

    public void SetEnabled(bool value)
    {
        isEnabled = value; // Activate or deactivate the script.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEnabled)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered the building area");

            isPlayerInside = true;
            StartCoroutine(DeactivateAfterDelay(8f)); // Deactivate script after 2 seconds.

            MinimapRoutes minimapRoutes = FindObjectOfType<MinimapRoutes>();
            if (minimapRoutes != null)
            {
                minimapRoutes.StopCalculatingAndHideRotesToDestination();
                Debug.Log("Path Stopped");
            }

            DeactivateParticleHighlight();
            minimapRoutes.HideDistanceText();

            // Activate the canvas
            canvas.gameObject.SetActive(true);
            soundplayer.Play();
                    }
    }

    private void OnTriggerExit(Collider other)
    {
       

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left the building area");

            isPlayerInside = false;
            isEnabled = false;

            // Deactivate the canvas
            canvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Deactivate the script after 2 seconds when player is inside.
        if (isPlayerInside && isEnabled)
        {
            StartCoroutine(DeactivateAfterDelay(8f));
        }
    }
}
