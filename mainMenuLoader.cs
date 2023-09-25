using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuLoader : MonoBehaviour
{
    public void LoadMainMenu(int index)
    {
        SceneManager.LoadScene(index);
       
    }
}