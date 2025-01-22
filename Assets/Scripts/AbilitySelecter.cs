using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelecter : MonoBehaviour
{

    public Dash dash;
    public SlowMotion slowMotion;
    public RawImage crosshair;
    public FirstPersonLook FPL;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }


    public void DashSelect()
    {
        dash.enabled = true;
        slowMotion.enabled = false;
        Destroy(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }


    public void SlowMoSelect()
    {
        dash.enabled = false;
        slowMotion.enabled = true;
        Destroy(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
