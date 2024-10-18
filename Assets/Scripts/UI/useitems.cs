using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useitems : MonoBehaviour
{
    public Camera mainCamera;
    public RectTransform crosshairObject;
    public float maxDetectionDistance;
    private GameObject openKey;
    public Transform camera;
    public GameObject variables;
    public int deagleammo;
    public int woodgiven, tape, metal;
    public GameObject slide1, slide2, slide3, itemNameObj, itemCountObj;
    public Text itemName, itemCount, mintText, holder;
    public AudioSource bulletsSnd, woodSnd, metalSnd, tapeSnd;
    public GameObject mc;
    public GameObject wood, wood2;
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(crosshairObject.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDetectionDistance, LayerMask.GetMask("Item")))
        {
            if(hit.transform.gameObject.name == "hole")
            {
                if (hit.transform.gameObject.GetComponent<activateSeal>().seal == false)
                {
                    openKey = hit.transform.Find("SealPrompt").gameObject;
                    openKey.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        mc.transform.position = new Vector3(hit.transform.position.x, mc.transform.position.y, hit.transform.position.z - 0.546f);
                        mc.transform.rotation = new Quaternion(0, 0, 0, 0);
                        mc.GetComponent<thirdpersonmove>().repair();
                        mc.GetComponent<thirdpersonmove>().finishpart = hit.transform.Find("finishpart").gameObject;
                        mc.GetComponent<thirdpersonmove>().finishlight = hit.transform.Find("flash").gameObject;
                        mc.GetComponent<thirdpersonmove>().hole = hit.transform.gameObject;
                    }
                }
            }
            if(hit.transform.gameObject.name == "cooler")
            {
                if (hit.collider.gameObject.GetComponent<activateCooler>().active == true)
                {
                    if (Input.GetKey(KeyCode.F))
                    {
                        holder = itemName;
                        itemName = mintText;
                        itemCount.text = "";
                        StartCoroutine(showslides());
                    }
                }
            }
            //opening container
            if (hit.transform.gameObject.name == "container")
            {
                openKey = hit.transform.Find("KeyPrompt").gameObject;
                if (hit.transform.gameObject.GetComponent<activateContainer>().active == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.transform.gameObject.GetComponent<Animation>()["open"].speed = 0.5f;
                        hit.transform.gameObject.GetComponent<Animation>().Play("open");
                        deagleammo = Random.Range(0, 15);
                        variables.GetComponent<variables>().deagleammoowned = variables.GetComponent<variables>().deagleammoowned + deagleammo;
                        float givewood = Random.Range(0, 10);
                        Debug.Log(givewood);
                        woodgiven = Random.Range(1, 5);
                        variables.GetComponent<variables>().woodamount = variables.GetComponent<variables>().woodamount + woodgiven;
                        if (givewood <= 2)
                        {
                            tape = Random.Range(1, 2);
                            variables.GetComponent<variables>().tape = variables.GetComponent<variables>().tape + tape;
                        }
                        if (givewood >= 6)
                        {
                            metal = Random.Range(1, 3);
                            variables.GetComponent<variables>().metal = variables.GetComponent<variables>().metal + metal;
                        }
                        openKey.SetActive(false);
                        hit.transform.gameObject.GetComponent<activateContainer>().active = false;
                        if(givewood >= 3)
                        {
                            deagleammo = Random.Range(0, 15);
                        }
                        itemName.text = "WOOD SCRAPS";
                        itemCount.text = "(" + woodgiven.ToString() + ")";
                        woodSnd.Play();
                        StartCoroutine(showslides());
                    }
                    openKey.SetActive(true);
                }
                else
                {
                    if (!hit.transform.gameObject.GetComponent<Animation>().isPlaying)
                    {
                        hit.transform.gameObject.GetComponent<activateContainer>().timer.SetActive(true);
                    }
                }
            }
        }
        else
        {
            if(openKey != null)
            {
                openKey.SetActive(false);
            }
        }
    }
    void ammoSlides()
    {
        itemName.text = "DEAGLE AMMO";
        itemCount.text = "(" + deagleammo.ToString() + ")";
        bulletsSnd.Play();
        StartCoroutine(showslides());
        deagleammo = 0;
    }
    void tapeSlides()
    {
        itemName.text = "TAPE";
        itemCount.text = "(" + tape.ToString() + ")";
        tapeSnd.Play();
        StartCoroutine(showslides());
        tape = 0;
    }
    void metalSlides()
    {
        itemName.text = "METAL SCRAP";
        itemCount.text = "(" + metal.ToString() + ")";
        metalSnd.Play();
        StartCoroutine(showslides());
        metal = 0;
    }
    IEnumerator showslides()
    {
        yield return new WaitForSeconds(0.1f);
        slide1.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        slide2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        slide3.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        itemNameObj.SetActive(true);
        itemCountObj.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        slide1.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        slide2.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        slide3.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        itemNameObj.SetActive(false);
        itemCountObj.SetActive(false);
        if(deagleammo > 0)
        {
            ammoSlides();
        }
        else
        {
            if (tape > 0)
            {
                tapeSlides();
            }
            else
            {
                if (metal > 0)
                {
                    metalSlides();
                }
            }
        }
        if(itemName == mintText)
        {
            itemName = holder;
        }
    }
}