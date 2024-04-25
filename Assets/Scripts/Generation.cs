using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generation : MonoBehaviour
{

    [SerializeField] GameObject[] GroundsPrefabs; // On crée un tableau qui va récup les prefabs
    GameObject[] GroundsOnGame; // On crée un tableau qui va récup les prefabs mis en jeu
    
    GameObject player;
    GameObject bawl;
    [SerializeField] GameObject GroundCKP;
    [SerializeField] GameObject FisrtGround;
    
    int prevGround;
    private int lastRandomIndex;

    private float groundSize;
    private int groundCount;
    [SerializeField] int nbGrounds;

    void Start()
    {
        player = GameObject.Find("Player"); 
        bawl = GameObject.Find("Boule");
        
        GroundsOnGame = new GameObject[nbGrounds]; // On crée la liste et on l'instancie (équivaut à appuyer sur les plus dans l'inspector)

        for (int i=0; i<nbGrounds; i++)
        {
            GroundsOnGame[i] = Instantiate(GroundsPrefabs[i]); // On ajoute à la liste des intances de grounds dans l'ordre (en bref on fait sapwn les grounds)  
        }

        GameObject firstGroundInstance = Instantiate(FisrtGround);
        groundSize = GroundsOnGame[0].GetComponentInChildren<Transform>().Find("Road").localScale.z * 9; // On récup la taille du ground qui s'appel "road"
        
        float pos = player.transform.position.z + groundSize/2 - 1.5f; // Position de départ. Mettre groundsize/2 pour les test

        // On place le fisrtGround en premier
        firstGroundInstance.transform.position = new Vector3(0, 0.2f, pos); // On place le ground à la bonne hauteur et position
        pos += groundSize; // Puis on redef la posoition initiale des autres grounds

        foreach (var ground in GroundsOnGame)
        {
            ground.transform.position = new Vector3(0, 0.2f, pos); // On place le ground à la bonne hauteur et position
            pos += groundSize; // Chaque ground sera placé/positionné juste après son prédecesseur
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=GroundsOnGame.Length - 1; i>=0; i--)
        {
            GameObject ground = GroundsOnGame[i]; // On récup tous les grounds en jeu
            

            // Si le ground est dérrière le player
            if (ground.transform.position.z + groundSize/2 < bawl.transform.position.z - 6f) 
            {
                float z = ground.transform.position.z; // On save sa position
                
                Destroy(ground); // On le détruit

                // Choix du prefab à instancier en fonction du nbr d'itération
                GameObject prefabToInstantiate;
                if (groundCount % 15 == 0 ) // Si c'est le 15e ground 
                {
                    prefabToInstantiate = GroundCKP;
                    
                    if (prefabToInstantiate == null) // Si aucun prefab trouvé
                    {
                        lastRandomIndex = Random.Range(0, GroundsPrefabs.Length);
                        prefabToInstantiate = GroundsPrefabs[lastRandomIndex];
                    }
                }
                else // Sinon, choisir un prefab aléatoire différent
                {
                    do
                    {
                        lastRandomIndex = Random.Range(0, GroundsPrefabs.Length);
                        prefabToInstantiate = GroundsPrefabs[lastRandomIndex];
                    } while(lastRandomIndex == prevGround);
                }

                ground = Instantiate(prefabToInstantiate); // On le remplace
                groundCount++;
                ground.transform.position = new Vector3(0, 0.2f, z + groundSize * nbGrounds + 5); // On le positionne devant le dernier ground
                GroundsOnGame[i] = ground; // On ajoute le petit nouveau 
                prevGround = lastRandomIndex;
            }
        }
    }
    
}
