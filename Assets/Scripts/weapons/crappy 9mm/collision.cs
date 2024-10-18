using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 4f);
    }
    void OnCollisionEnter(Collision collison)
    {
        if(collison.gameObject.tag == "surface")
        {
            gameObject.SetActive(false);
        }
    }
}
