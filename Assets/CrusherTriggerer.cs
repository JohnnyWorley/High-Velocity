using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherTriggerer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CrusherDelay());
        }
    }

    IEnumerator CrusherDelay()
    {
        yield return new WaitForSeconds(0.2f);
        CrusherStart();
    }

    void CrusherStart()
    {
        Transform[] transform = GetComponentsInChildren<Transform>();
        foreach (Transform t in transform)
        {
            if (t.GetComponent<Animator>() != null)
            {
                Animator animator = t.GetComponent<Animator>();
                animator.SetTrigger("Trigger");
            }
        }
        GetComponent<BoxCollider>().enabled = false;
    }
}
