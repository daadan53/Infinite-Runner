using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDir;
    private string dataFileName;

    public FileDataHandler(string dataDir, string dataFileName)
    {
        this.dataDir = dataDir;
        this.dataFileName = dataFileName;
    }

    // Recup les données save 
    public GameData Load()
    {
        string fullPath = dataDir + "/" + dataFileName;

        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad=""; 

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // On désérialize 
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                
                Debug.LogError("Erreur lors du chargement au fichier : " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    // Save les données et crée le fichier de save
    public void Save(GameData data)
    {
        string fullPath = dataDir + "/" + dataFileName;

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); // Crée un fichier dans le repertoir voulu en mode ecriture

            string dataToStore = JsonUtility.ToJson(data, true); // On convertie les données en JSON

            // On écrie dans ce fichier
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            
            Debug.LogError("Erreur lors de la sauvegarde au fichier : " + fullPath + "\n" + e);
        }
    
    }
}

