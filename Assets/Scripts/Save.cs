using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*public class Save : MonoBehaviour
{
    public SaveData data;
    public static Save saveInstance;
    
    private void Awake() 
    {
        if (saveInstance != null)
        {
            Destroy(gameObject);
        }

        saveInstance = this;
    }

    public void SaveGame()
    {
        data = SaveData.saveDataInstance;
        
        // Dir = C:\Users\danma\AppData\LocalLow\DefaultCompany\Infinite Runner
        string dir = Application.persistentDataPath + "/Saves"; 

        // On crée le dossier
        if(!Directory.Exists(dir)) 
        {
            Directory.CreateDirectory(dir);
        }

        // On converti la classe en string 
        string json = JsonUtility.ToJson(data); 

        // On écrit tout ca dans un fichier
        File.WriteAllText(dir + "/save1.json", json);

        Debug.Log("C'est save");
    }

    public void LoadGame()
    {
        string saveFilePath = Application.persistentDataPath + "/Saves/save1.json";

        if(File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);

            data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("C'est chargé");
        } else {Debug.Log("Pas de save dispo");}
    }

}*/
