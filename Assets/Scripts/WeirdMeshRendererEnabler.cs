using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdMeshRendererEnabler : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
