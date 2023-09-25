
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject SettingsMenu;
    private bool isPaused = false;

    public AudioMixer audioMixer;
    

    private void Start()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(false);
    }

     private void Update()
    {
        // Check for escape key press to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f; //pause the game
            isPaused = true;
        }
    }
    public void Resume()
    {
        if (isPaused)
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f; //resume the game
            isPaused = false;
        }
    }
    public void Settings()
    {
        SettingsMenu.SetActive(true);
    }
    public void SettingsCloseButton()
    {
        SettingsMenu.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit(); //quits the game
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
