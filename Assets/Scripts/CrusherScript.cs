using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherScript : MonoBehaviour
{
   public  bool ableToKill = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (ableToKill && collision.transform.CompareTag("Player"))
        {
           GameManager.instance.Death("Player was pulverized");
            GameManager.instance.crusherDeathBlack.gameObject.SetActive(true);
        }
    }



    public void killEnable()
    {
        ableToKill = true;
    }
    public void killDisable()
    {
        ableToKill = false;
    }
}
