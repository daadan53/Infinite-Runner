using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int oldPreviousScore;
    public int newPreviousScore;
    public Vector3 playerPos;
    public int topScore;

    // Mettre ici les valeurs initiale pour le new game
    public GameData()
    {
        this.oldPreviousScore = 0;
        this.newPreviousScore = 0;
        this.topScore = 0;
        //this.playerPos = new Vector3(0,2,0);
    }
}
