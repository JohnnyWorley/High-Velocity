using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour
{
    public GameObject player;
    public Image dashUI;
    public GameObject weaponHolder;
    public static WeaponsManager instance;
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject sniper;



    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



   public void WeaponsPickUp(string weaponTag)
    {
        switch (weaponTag)
        {
            case ("Pistol"):
                pistol.transform.parent = weaponHolder.transform;
                WeaponSwitcher.instance.SelectWeapon();
               
                OnScreenTexChanger.tutorialTextChanger.tutorialUI.text = "Press " + PlayerPrefs.GetString("shootKeybind") + " to shoot and scroll wheel to switch weapons";
                StartCoroutine(OnScreenTexChanger.tutorialTextChanger.BackgroundTimer());
                

                break;



            case ("Shotgun"):
                shotgun.transform.parent = weaponHolder.transform;
                WeaponSwitcher.instance.currentWeapon++;
                WeaponSwitcher.instance.SelectWeapon();
                break;



            case ("Sniper"):
                sniper.transform.parent = weaponHolder.transform;
                WeaponSwitcher.instance.currentWeapon++;
                WeaponSwitcher.instance.SelectWeapon();
                OnScreenTexChanger.tutorialTextChanger.tutorialUI.text = "Right mouse button to use scope";
                StartCoroutine(OnScreenTexChanger.tutorialTextChanger.BackgroundTimer());
                break;



            case ("SlowMotion"):
                player.GetComponent<SlowMotion>().enabled = true;
                GameObject.Find("TutorialDoor_2").GetComponent<Animator>().SetTrigger("Door");
                OnScreenTexChanger.tutorialTextChanger.tutorialUI.text = "Press " + PlayerPrefs.GetString("abilityKeybind") + " to use slow motion";
                StartCoroutine(OnScreenTexChanger.tutorialTextChanger.BackgroundTimer());
                break;



            case ("Dash"):
                dashUI.gameObject.SetActive(true);
                player.GetComponent<Dash>().enabled = true;
                GameObject.Find("TutorialDoor_2").GetComponent<Animator>().SetTrigger("Door");
                OnScreenTexChanger.tutorialTextChanger.tutorialUI.text = "Press " + PlayerPrefs.GetString("abilityKeybind") + " to use ability";
                StartCoroutine(OnScreenTexChanger.tutorialTextChanger.BackgroundTimer());
                break;
  
        }
    }
}
