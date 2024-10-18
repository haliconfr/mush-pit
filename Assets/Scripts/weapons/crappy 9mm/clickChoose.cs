using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickChoose : MonoBehaviour
{
    public GameObject variables;
    public AudioSource empty;
    public AudioSource full;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (variables.GetComponent<variables>().deagleammoloaded > 1)
        {
            full.Play();
        }
        else
        {
            empty.Play();
        }
    }
}
