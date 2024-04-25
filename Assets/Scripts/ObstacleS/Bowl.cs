using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    public static Bowl instanceBall;
    private GameObject player;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float distToPlayer;

    private void Awake() 
    {
        if (instanceBall != null)
        {
            Destroy(gameObject);
        }

        instanceBall = this;
        
    }

    void Start()
    {
        player = GameObject.Find("Player"); 

        distToPlayer = 60;
        transform.position = new Vector3(0,transform.localScale.y/2,player.transform.position.z - distToPlayer);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, transform.localScale.y / 2, 
        player.transform.position.z - distToPlayer), moveSpeed * Time.deltaTime);
    }

    // Fonction qui va modifié disToPlayer
    internal void BallNext(int pLife)
    {
        if(pLife <= 0)
        {
            distToPlayer = 0;
        } else
        {
            distToPlayer = distToPlayer/2;
        }
        
    }

    private void OnDestroy() 
    {
        instanceBall = null;
    }
}
