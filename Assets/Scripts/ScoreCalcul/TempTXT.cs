using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempTXT : MonoBehaviour, IDataPers
{
    private Stopwatch stopwatch;
    private float tempEcoule = 0;
    private int oldPreviousScore; // Le score qui sera enregistré
    private int newPreviousScore; // Le score qui sera affiché
    private TextMeshProUGUI tempTxt;
    private int topScore;
    private string txtTopScore;

    private void Awake() 
    {
        tempTxt = this.GetComponent<TextMeshProUGUI>();
    }

    public void LoadData(GameData data)
    {
        this.oldPreviousScore = data.oldPreviousScore; // On load le temps ecoulé de GameData à ici
        this.newPreviousScore = data.newPreviousScore;
        this.topScore = data.topScore;
    }

    public void SaveData(ref GameData data)
    {
        data.oldPreviousScore = (int)this.tempEcoule; // On envoi l'info du temps ecoule à GameData.tempEcoule
        data.newPreviousScore = this.oldPreviousScore;
        data.topScore = this.topScore;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GamePlay") 
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            tempEcoule = oldPreviousScore; // Le previous score devient le score actuelle à chaque mort
        }
        
    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            tempEcoule = (float)stopwatch.Elapsed.TotalSeconds;
        }

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            tempTxt.text = "Temps actuel : " + (int)tempEcoule + "\nTemps précedent : " + oldPreviousScore + "\nTop score : " + topScore;
        }
        else if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            tempTxt.text = "Temps actuel : " + (int)tempEcoule;
        }
        else if (SceneManager.GetActiveScene().name == "GameOver")
        {
            tempTxt.text = "Temps actuel : " + (int)oldPreviousScore + "\nTemps précedent : " + newPreviousScore + txtTopScore + topScore; // C'est l'inverse le new devient old mais bon flemme de tout changer
        }

        if(oldPreviousScore > topScore)
        {
            topScore = oldPreviousScore;
            txtTopScore = "\nNouveau Top score : ";
        }
        else if (oldPreviousScore < topScore) 
        {
            txtTopScore = "\nTop score : ";
            
        }

    }
}
