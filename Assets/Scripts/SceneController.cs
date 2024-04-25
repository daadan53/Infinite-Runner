using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    private void Awake() 
    {
        Instance = this;
    }
    public void ChangeScene()
    {
        // index de la scène actuelle
        int sceneCouranteIndex = SceneManager.GetActiveScene().buildIndex;

        // Charge la scène suivante
        SceneManager.LoadScene(sceneCouranteIndex + 1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0); 
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
