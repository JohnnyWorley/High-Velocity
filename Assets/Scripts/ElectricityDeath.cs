using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityDeath : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")){GameManager.instance.Death("Player received 1000 volts");}
    }
}
