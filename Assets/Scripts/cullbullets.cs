using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullbullets : MonoBehaviour
{
    float shrinkspeed = 0.1f;
    public Transform arm;
    Vector3 startscale;
    Vector3 originalscale;
    Vector3 targetscale = new Vector3(0,0,0);
    void OnEnable()
    {
        originalscale = transform.localScale;
        gameObject.transform.parent = null;
        startscale = transform.localScale;
        StartCoroutine(cull());
    }
    IEnumerator cull()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(shrink());
    }
    IEnumerator shrink()
    {
        yield return new WaitForEndOfFrame();
        transform.localScale = Vector3.Lerp(startscale, targetscale, shrinkspeed);
        if (gameObject.transform.localScale != targetscale)
        {
            shrinkspeed = shrinkspeed + 0.1f;
            StartCoroutine(shrink());
        }
        else
        {
            shrinkspeed = 0.1f;
            gameObject.transform.parent = arm;
            transform.localScale = originalscale;
            gameObject.SetActive(false);
        }
    }
}
