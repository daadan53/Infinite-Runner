using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName; // Nom du fichier de save

    public static DataManager Instance { get; private set;} // On ne peut que l'use et pas le modif
    private GameData gameData;
    private List<IDataPers> dataPersObj; // On récup toutes les données save ?
    private FileDataHandler dataHandler;

    private void Awake() 
    {
        if(Instance != null)
        {
            Debug.LogError("Il y a plusieurs instances de Data Manager qui ont été trouvé sur la scène");
        }
        Instance = this;
    }

    private void Start() 
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName); // Instanciation par le construct
        this.dataPersObj = FindAllDataPersObj();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData(); // On insatancie les data
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if(this.gameData == null)
        {
            Debug.Log("Pas de sauvegarde trouvé");
            NewGame();
        }

        foreach(IDataPers dataPersObj in dataPersObj)
        {
            dataPersObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach(IDataPers dataPersObj in dataPersObj)
        {
            dataPersObj.SaveData(ref gameData);
        }
        
        dataHandler.Save(gameData);
        Debug.Log("Ca save");
    }

    /*private void OnApplicationQuit() 
    {
        SaveGame();
    }*/

    private List<IDataPers> FindAllDataPersObj()
    {
        IEnumerable<IDataPers> dataPersObj = FindObjectsOfType<MonoBehaviour>().OfType<IDataPers>();

        return new List<IDataPers>(dataPersObj);
    }
}
