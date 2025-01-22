using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColourChanger : MonoBehaviour
{
    int lower = 0;
    int upper = 255;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<SkinnedMeshRenderer>().material.color = new Color(Random.Range(lower, upper), Random.Range(lower, upper), Random.Range(lower, upper));
    }
}
