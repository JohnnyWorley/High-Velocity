using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(particleDestroy());
    }

    private IEnumerator particleDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
