using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject weaponCamera;

    public int currentWeapon = 0;
    public Image sniperOverlay;
    public SniperScript sniperScript;
    public static WeaponSwitcher instance;


    void Start()
    {
        
       // weaponImages = GetComponentsInChildren<Image>();
        instance = this;
        SelectWeapon();
    }

    void Update()
    {

        int prevSelectedWeapon = currentWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            weaponCamera.SetActive(true);
            sniperOverlay.gameObject.SetActive(false);
            sniperScript.scoped = false;
            Camera.main.fieldOfView = PlayerPrefs.GetFloat("fov");
            if (currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weaponCamera.SetActive(true);
            sniperOverlay.gameObject.SetActive(false);
            Camera.main.fieldOfView = PlayerPrefs.GetFloat("fov");
            sniperScript.scoped = false;
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                currentWeapon--;
            }
        }

        if (prevSelectedWeapon != currentWeapon)
        {
            SelectWeapon();
        }

    }

    public void SelectWeapon()
    {
        int i = 0;
      
        foreach (Transform weapon in transform)
        {
            if (i == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

}
