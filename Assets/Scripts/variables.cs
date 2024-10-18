using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class variables : MonoBehaviour
{
    public float sensitivity, deagleammoloaded, deagleammoowned;
    public string weaponholding, animMeantToPlay;
    public GameObject deagle, gore;
    public int woodamount, tape, metal, shotsfired;
    public Text text;
    public void Start()
    {
        weaponholding = "deagle";
        deagle.SetActive(true);
    }
    public void Update()
    {
        text.text = deagleammoowned.ToString();
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponholding = "deagle";
            deagle.SetActive(true);
            gore.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponholding = "gore";
            gore.SetActive(true);
            deagle.SetActive(false);
        }
    }
}
