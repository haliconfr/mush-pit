using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useweapons : MonoBehaviour
{
    public GameObject torso;
    public GameObject camera;
    public GameObject variables;
    public GameObject deagle;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(variables.GetComponent<variables>().weaponholding == "deagle")
            {
                deagle.GetComponent<fire>().shoot();
            }
        }
    }
    private void usehammer()
    {
       
    }
}
