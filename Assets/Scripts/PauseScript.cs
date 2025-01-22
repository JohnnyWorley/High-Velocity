using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public Volume volume;
    public GameObject cameraHolder;
    private Quaternion prePauseRot;
    public SniperScript sniperScript;
    private GameObject playerHUD;
    public RawImage crosshair;
    public static PauseScript instance;
    public static bool pauseAllowed = true;
    public static bool isPaused = false;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    void Start()
    {
        playerHUD = GameObject.Find("GameHUD");
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // 
        if (Input.GetKeyDown(KeyCode.Escape) && !TimerScript.instance.levelEnded && GameManager.instance.isAlive)
        { 
            if (isPaused)
            {
               Unpause();
            }
            else
            {
                Pause();
            }
        }
    }




    public void Unpause()
    {
        BlurDisable();
        playerHUD.SetActive(true);
        crosshair.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        BlurEnable();
        prePauseRot = Camera.main.transform.rotation;
        cameraHolder.transform.rotation = prePauseRot;
        sniperScript.UnScope();
        playerHUD.SetActive(false);
        crosshair.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }


    public void BlurEnable()
    {
        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.active = true;
        }
    }
    public void BlurDisable()
    {
        DepthOfField dof;
        if (volume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.active = false;
        }
    }
}
