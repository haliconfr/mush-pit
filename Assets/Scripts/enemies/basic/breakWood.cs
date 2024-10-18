using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWood : MonoBehaviour
{
    GameObject sealWood1;
    GameObject sealWood2;
    GameObject brokenWood1;
    GameObject brokenWood2;
    public GameObject enemyBase, player;
    LayerMask whatIsPlayer;
    void BreakWood()
    {
        sealWood1 = GameObject.Find("wood (1)(Clone)").gameObject;
        sealWood2 = GameObject.Find("wood(Clone)").gameObject;
        brokenWood1 = enemyBase.GetComponent<BaseEnemyAI>().entry.Find("broken wood 1").gameObject;
        brokenWood2 = enemyBase.GetComponent<BaseEnemyAI>().entry.Find("broken wood 2").gameObject;
        sealWood1.SetActive(false);
        brokenWood1.SetActive(true);
    }
    void BreakWood2()
    {
        sealWood2.SetActive(false);
        brokenWood2.SetActive(true);
        //enemyBase.GetComponent<BaseEnemyAI>().entry.GetComponent<activateSeal>().seal = false;
    }
    void dealDamage()
    {
        player.GetComponent<thirdpersonmove>().health--;
    }
}
