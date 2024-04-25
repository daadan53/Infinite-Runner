using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleState : MonoBehaviour
{

    [SerializeField] private float speedMoove;
    [SerializeField] private int dist;
    private Transform playerPos;
    [Range(0.5f,50)] public float detectDistance = 20;
    private Vector3 verticalSize;
    public PlayerController playerController;

    void Start()
    {
        speedMoove = Random.Range(5,11);
        dist = Random.Range(11, 15);

        playerPos = GameObject.Find("Player").transform;
        playerController = PlayerController.Instance;

        verticalSize = gameObject.transform.localScale;
        if(playerController.sizeState)
        {
            gameObject.transform.localScale = Vector3.one;
        }
    }

    void Update()
    {
        if(!playerController.sizeState)
        {
            
            if(gameObject.tag == "Horizontal")
            {
                gameObject.transform.localScale = new Vector3(10,10,10);
                transform.Translate(Vector3.right * speedMoove * Time.deltaTime);
                if(transform.position.x >= dist || transform.position.x <= -dist)
                {
                    speedMoove = -speedMoove;
                }
            }
            else if(gameObject.tag == "Vertical")
            {
                gameObject.transform.localScale = verticalSize;
            }
        }
        else
        {
            SetSize();
        }
        
    }

    private void SetSize()
    {
        if(Vector3.Distance(transform.position, playerPos.position) <= detectDistance + 1)
        {
            if(gameObject.tag == "Horizontal")
            {
                //Effet visuel
                iTween.ScaleTo(gameObject, new Vector3(10,10,10), 0.5f);
            }
            else if(gameObject.tag == "Vertical")
            {
                iTween.ScaleTo(gameObject, verticalSize, 0.5f);
            }
        }
        else
        {
            gameObject.transform.localScale = Vector3.one;
        }
    }
}
