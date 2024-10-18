using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveClouds : MonoBehaviour
{
    float time;
    void Start()
    {
        time = Random.Range(0.0001f, 0.002f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + time);
        if(transform.position.z >= 552f)
        {
            transform.position = new Vector3(transform.position.x, Random.Range(52.7f, 131.8f), -548f);
        }
    }
}
