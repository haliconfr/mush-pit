using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class fire : MonoBehaviour
{
    public Animation anim;
    public GameObject part;
    public float bulletspeed;
    public GameObject bullet;
    public GameObject mc;
    public Transform gun;
    public GameObject shot;
    public GameObject cam;
    public GameObject variables;
    public Animation thisanim;
    public Animation mcanim;
    public Text text;
    public GameObject fixprompt, fixkey;
    public bool broken;
    public ParticleSystem smoke;
    private void Start()
    {
        thisanim = gameObject.GetComponent<Animation>();
        mcanim = mc.GetComponent<Animation>();
        smoke.Play();
    }
    private void Update()
    {
        if (broken == true)
        {
            fixprompt.SetActive(true);
            fixkey.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (variables.GetComponent<variables>().metal >= 5)
                {
                    variables.GetComponent<variables>().metal = variables.GetComponent<variables>().metal - 5;
                    fixprompt.SetActive(false);
                    fixkey.SetActive(false);
                    broken = false;
                    variables.GetComponent<variables>().shotsfired = 0;
                }
            }
        }
    }
    public void reload()
    {
        thisanim.Stop();
        thisanim["deaglelid"].speed = 1.0f;
        thisanim["deaglelid"].time = thisanim["deaglelid"].length;
        thisanim.Play("deaglelid");
    }
    public void animreset()
    {
        thisanim["deaglelid"].speed = -1.0f;
        thisanim["deaglelid"].time = gameObject.GetComponent<Animation>()["deaglelid"].length;
        thisanim.Play("deaglelid");
    }
    public void shoot()
    {
        if (broken != true)
        {
            if (!mc.GetComponent<thirdpersonmove>().inaction)
            {
                if (variables.GetComponent<variables>().deagleammoloaded != 0)
                {
                    mcanim.Stop("shoot");
                    GameObject dupe = Instantiate(bullet, gun);
                    dupe.SetActive(true);
                    dupe.name = Random.Range(0, 90).ToString();
                    dupe.transform.parent = null;
                    dupe.GetComponent<Rigidbody>().AddForce(transform.forward * (bulletspeed * 100));
                    thisanim.Stop();
                    thisanim.Play("fire");
                    mcanim["shoot"].layer = 1;
                    mcanim["runtorso"].layer = 0;
                    mcanim.Play("shoot");
                    GameObject audio = Instantiate(shot, gun);
                    audio.GetComponent<AudioSource>().Play();
                    Destroy(audio, 4f);
                    GameObject system = Instantiate(part, gun);
                    Destroy(system, 1f);
                    system.SetActive(true);
                    system.transform.parent = null;
                    system.GetComponent<ParticleSystem>().Play();
                    variables.GetComponent<variables>().deagleammoloaded = variables.GetComponent<variables>().deagleammoloaded - 1;
                    variables.GetComponent<variables>().shotsfired++;
                    text.text = variables.GetComponent<variables>().deagleammoowned.ToString();
                    smoke.emissionRate = variables.GetComponent<variables>().shotsfired / 10;
                    smoke.startSpeed = variables.GetComponent<variables>().shotsfired / 25;
                    if (variables.GetComponent<variables>().shotsfired == 50)
                    {
                        broken = true;
                    }
                }
                else
                {
                    if (variables.GetComponent<variables>().deagleammoowned != 0)
                    {
                        mcanim.Play("reload");
                    }
                }
            }
            else
            {
                thisanim.Stop();
            }
        }
    }
}
