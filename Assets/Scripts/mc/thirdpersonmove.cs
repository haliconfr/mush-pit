using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdpersonmove : MonoBehaviour
{
    public CharacterController me;
    public GameObject cam, gun, torso;
    public float speed;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    public bool moving;
    private Animation anim;
    public bool sprint;
    public bool sealing;
    public int countdown;
    public int health = 10;
    public int stop;
    public bool stopcounting;
    public bool animstart;
    public bool inaction;
    public AudioSource plasticlick;
    public GameObject variables, wood, wood1, hole;
    public bool started;
    public bool shouldbezoomed;
    public Vector3 direction;
    public GameObject finishpart,finishlight,healthfull,health4,health3,health2,health1,healthempty,separatorkey,separatorprompt,separatorlight,separatorpart,vendorkey,vendorprompt;
    public AudioSource completeseal;
    public LayerMask separator,item;
    RaycastHit hit;
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, separator))
        {
            separatorkey.SetActive(true);
            separatorprompt.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                separatorlight = hit.collider.transform.Find("flashSeparator").gameObject;
                separatorpart = hit.collider.transform.Find("finishSeparator").gameObject;
                separatorlight.SetActive(true);
                separatorlight.transform.position = new Vector3(separatorlight.transform.position.x, separatorlight.transform.position.y, transform.position.z);
                separatorlight.GetComponent<Light>().intensity = 1.5f;
                StartCoroutine(sepIntense());
                hit.collider.gameObject.GetComponent<Animation>()["open"].speed = 0.7f;
                hit.collider.gameObject.GetComponent<Animation>().Play("open");
                hit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            separatorkey.SetActive(false);
            separatorprompt.SetActive(false);
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.5f, item))
        {
            if(hit.collider.gameObject.name == "cooler")
            {
                if (hit.collider.gameObject.GetComponent<activateCooler>().active == true)
                {
                    vendorprompt.SetActive(true);
                    vendorkey.SetActive(true);
                    if (Input.GetKey(KeyCode.F))
                    {
                        hit.collider.gameObject.GetComponent<Animation>()["vendoropen"].speed = 0.7f;
                        hit.collider.gameObject.GetComponent<Animation>().Play("vendoropen");
                        hit.collider.gameObject.GetComponent<activateCooler>().active = false;
                    }
                }
            }
        }
        else
        {
            vendorkey.SetActive(false);
            vendorprompt.SetActive(false);
        }
        Debug.Log(health);
        if(health == 10 || health == 9)
        {
            healthfull.SetActive(true);
        }
        if (health == 8 || health == 7)
        {
            healthfull.SetActive(false);
            health4.SetActive(true);
        }
        if (health == 6 || health == 5)
        {
            health4.SetActive(false);
            health3.SetActive(true);
        }
        if (health == 4 || health == 3)
        {
            health3.SetActive(false);
            health2.SetActive(true);
        }
        if (health == 2 || health == 1)
        {
            health2.SetActive(false);
            health1.SetActive(true);
        }
        if (health == 0)
        {
            health1.SetActive(false);
            healthempty.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!anim.IsPlaying("reload"))
            { 
                anim.Play("reload");
            }
        }
        anim = GetComponent<Animation>();
        float horizontal = Input.GetAxisRaw("Horizontal") * 5.0f;
        float vertical = Input.GetAxisRaw("Vertical") * 5.0f;
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (!inaction)
        {
            if (direction.magnitude >= 0.1f)
            {
                stopcounting = false;
                    countdown = 0;
                    stop = 0;
                    if (speed == 3f)
                    {
                        if (!anim.IsPlaying("walk"))
                        {
                            anim.CrossFade("walk", 0.1f);
                            anim["walktorso"].layer = 1;
                                if (variables.GetComponent<variables>().weaponholding == "deagle")
                                {
                                    anim.CrossFade("walktorso", 0.1f);
                                }
                                if (variables.GetComponent<variables>().weaponholding == "gore")
                                {
                                    anim.CrossFade("run with hammer torso", 0.1f);
                                }
                        }
                    }
                    if (speed == 2f)
                    {
                        if (anim["walk"].speed == 0.5f)
                        {
                            anim["walk"].speed = 0.5f;
                            anim.CrossFade("walk", 0.1f);
                        }
                    }
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        if (!anim.IsPlaying("run"))
                            {
                                if (variables.GetComponent<variables>().weaponholding == "deagle")
                                {
                                    anim["run"].speed = 0.9f;
                                    anim.CrossFade("run", 0.1f);
                                    speed = 6f;
                                    anim["runtorso"].speed = 0.9f;
                                        anim["runtorso"].layer = 1;
                                        anim.CrossFade("runtorso", 0.1f);
                                }
                                if (variables.GetComponent<variables>().weaponholding == "gore")
                                {
                                    anim["run"].speed = 0.5f;
                                    anim.CrossFade("run", 0.1f);
                                    speed = 4f;
                                    anim["run with hammer torso"].speed = 0.9f;
                                        anim["run with hammer torso"].layer = 1;
                                        anim.CrossFade("run with hammer torso", 0.1f);
                                }
                            }
                            sprint = true;
                    }
                    else
                    {
                        speed = 3f;
                        sprint = false;
                    }
                    moving = true;
                    Vector3 forward = torso.transform.forward;
                    Vector3 backward = -torso.transform.forward;
                    Vector3 right = torso.transform.right;
                    Vector3 left = -torso.transform.right;
                    if(Input.GetKey(KeyCode.W)){
                        me.Move((backward+=Physics.gravity) * speed * Time.deltaTime);
                    }
                    if(Input.GetKey(KeyCode.S)){
                        me.Move((forward+=Physics.gravity) * speed * Time.deltaTime);
                    }
                    if(Input.GetKey(KeyCode.A)){
                        me.Move((right+=Physics.gravity) * speed * Time.deltaTime);
                    }
                    if(Input.GetKey(KeyCode.D)){
                        me.Move((left+=Physics.gravity) * speed * Time.deltaTime);
                    }
            }
            else
            {
                moving = false;
                if (!stopcounting)
                {
                    countdown++;
                }
                if (countdown >= 5000)
                {
                    stopcounting = true;
                    if (!anim.isPlaying)
                    {
                        if (variables.GetComponent<variables>().weaponholding == "deagle")
                        {
                            anim.CrossFade("9mm idle", 0.1f);
                        }
                    }
                    countdown = 0;
                }
                if (stop != 1)
                {
                    anim.Stop();
                    if (variables.GetComponent<variables>().weaponholding == "gore")
                    {
                        anim.Play("gore idle");
                    }
                    if (variables.GetComponent<variables>().weaponholding == "deagle")
                    {
                        anim.Play("idle");
                    }
                    stop = 1;
                }
            }
        }
    }
    public void zoom()
    {
        anim["9mm zoom"].speed = -1.0f;
        anim["9mm zoom"].layer = 1;
        anim["9mm zoom"].time = anim["9mm zoom"].length;
        anim.CrossFade("9mm zoom", 0.1f);
    }
    public void reload()
    {
        int loaded = (int)variables.GetComponent<variables>().deagleammoloaded;
        int owned = (int)variables.GetComponent<variables>().deagleammoowned;
        int diff = 15 - loaded;
        diff = Mathf.Min(diff, owned);
        loaded += diff;
        owned -= diff;
        variables.GetComponent<variables>().deagleammoloaded = loaded;
        variables.GetComponent<variables>().deagleammoowned = owned;
    }
    public void resetanim()
    {
        gun.GetComponent<fire>().animreset();
        plasticlick.Play();
    }
    public void releasebullets()
    {
        anim["activatebullets"].layer = 1;
        anim.Play("activatebullets");
    }
    public void lid()
    {
        gun.GetComponent<fire>().reload();
    }
    public void repair()
    {
        anim["seal entry"].speed = 0.7f;
        anim.CrossFade("seal entry", 0.1f);
        sealing = true;
        inaction = true;
    }
    public void LateUpdate()
    {
        if (sealing == true)
        {
            if (!anim.IsPlaying("seal entry"))
            {
                GameObject woodDupe = Instantiate(wood, null);
                GameObject woodDupe1 = Instantiate(wood1, null);
                woodDupe.transform.position = wood.transform.position;
                woodDupe1.transform.position = wood1.transform.position;
                woodDupe.transform.localScale = new Vector3(0.01625822f, 0.8427588f, 0.1017377f);
                woodDupe1.transform.localScale = new Vector3(0.01625822f, 0.8427588f, 0.1017377f);
                wood.SetActive(false);
                wood1.SetActive(false);
                if (variables.GetComponent<variables>().weaponholding == "deagle")
                {
                    anim.CrossFade("idle", 0.1f);
                }
                if (variables.GetComponent<variables>().weaponholding == "gore")
                {
                    anim.CrossFade("gore idle", 0.1f);
                }
                finishpart.GetComponent<ParticleSystem>().Play();
                finishlight.SetActive(true);
                finishlight.GetComponent<Light>().intensity = 1.5f;
                StartCoroutine(intense());
                completeseal.Play();
                hole.GetComponent<activateSeal>().seal = true;
                sealing = false;
                inaction = false;
            }
        }
    }
    IEnumerator intense()
    {
        yield return new WaitForSeconds(0.05f);
        if (finishlight.GetComponent<Light>().intensity > 0)
        {
            finishlight.GetComponent<Light>().intensity = finishlight.GetComponent<Light>().intensity - 0.1f;
            StartCoroutine(intense());
        }
    }
    IEnumerator sepIntense()
    {
        yield return new WaitForSeconds(0.05f);
        if (separatorlight.GetComponent<Light>().intensity > 0)
        {
            separatorlight.GetComponent<Light>().intensity = separatorlight.GetComponent<Light>().intensity - 0.1f;
            StartCoroutine(sepIntense());
        }
    }
}