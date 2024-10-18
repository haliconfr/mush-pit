using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class createMap : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(startMap());

    }
    IEnumerator startMap()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (var box in gameObject.GetComponents<BoxCollider>())
        {
            box.enabled = true;
        }

    }
}
