using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWindow : MonoBehaviour
{
    private Collision varCollision;
    bool hit;
    public GameObject brokenWindow;
     void OnCollisionEnter(Collision collision)
    {
        if (!hit)
        {
            if (collision.transform.CompareTag("Player") || (collision.transform.CompareTag("Bullet")))
            {
                hit = true;
                varCollision = collision;
                WindowBreakFunction();
            }
        }
       
    }  
    
    public void WindowBreakFunction()
    {

        GameObject brokenGlass = Instantiate(brokenWindow, transform.position, transform.rotation);
        Transform[] glassShards;
        glassShards = brokenGlass.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform glassShardTransforms in glassShards)
        {
            Rigidbody rb = glassShardTransforms.GetComponentInChildren<Rigidbody>();
            if (varCollision != null)
            {
                rb.AddForce(varCollision.transform.forward * 200);

            }
            else
            {
                rb.AddForce(transform.forward * 200);
            }
        }
        brokenGlass.transform.localScale = transform.localScale;
        brokenGlass.transform.position = transform.position;
        Destroy(gameObject);
    }

}
