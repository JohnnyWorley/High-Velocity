using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LerpTest : MonoBehaviour
{


    public float tiltAngle;
    public float lerpAngle;
    public bool aHeld;
    public bool dHeld;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        if (Input.GetKey(KeyCode.A) && !dHeld)
        {
            aHeld = true;
            tiltAngle = Mathf.Lerp(tiltAngle, 3f, 1*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) && !aHeld) 
        {
            dHeld = true;
            tiltAngle = Mathf.Lerp(tiltAngle, -3f, 1 * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            aHeld = false;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            dHeld = false;
        }

        if (!dHeld && !aHeld)
        {
            tiltAngle = Mathf.Lerp(tiltAngle, 0, 1 * Time.deltaTime);
        }

    }
}
