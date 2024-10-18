using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activateContainer : MonoBehaviour
{
    public bool active;
    public bool timerstarted;
    public GameObject timer;
    float time;
    public Animation anim;
    public void Start()
    {
        active = true;
        timer = transform.Find("timer").gameObject;
        anim = gameObject.GetComponent<Animation>();
    }
    private void Update()
    {
        if(active == false)
        {
            if(timerstarted == false)
            {
                time = 120;
                StartCoroutine(countdown());
                timerstarted = true;
            }
        }
        else
        {
            timer.SetActive(false);
        }
    }
    void updateTimer()
    {
        timer.GetComponent<TextMesh>().text = time.ToString();
        if (time == 0)
        {
            anim["open"].speed = -1.0f;
            anim["open"].time = anim["open"].length;
            anim.Play("open");
            active = true;
        }
        else
        {
            StartCoroutine(countdown());
        }
    }
    IEnumerator countdown()
    {
        yield return new WaitForSeconds(1);
        time--;
        updateTimer();
    }
}
