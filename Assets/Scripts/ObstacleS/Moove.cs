using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moove : MonoBehaviour
{
    private float speedMoove;
    private float dist;

    void Start()
    {
        speedMoove = Random.Range(5,11);
        dist = 10;
    }

    void Update()
    {

        transform.Translate(Vector3.right * speedMoove * Time.deltaTime);

        if(transform.position.x >= dist || transform.position.x <= -dist)
        {
            speedMoove = -speedMoove;
        }
    }
}
