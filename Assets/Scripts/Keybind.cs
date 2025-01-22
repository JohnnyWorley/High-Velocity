using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Linq;

public class Keybind : MonoBehaviour
{
    public TextMeshProUGUI shootButton;
    public TextMeshProUGUI rocketButton;
    public TextMeshProUGUI abilityButton;
    public TextMeshProUGUI pickUpButton;


    public string identifier;
    bool clicked = false;
    void Start()
    {
       // rocketButton.text = GameManager.instance.rocketKeybind.ToString();
       // abilityButton.text = GameManager.instance.abilityKeybind.ToString();
       //  pickUpButton.text = GameManager.instance.pickUpKeyBind.ToString();
    }

    // Update is called once per frame 
    void Update()
    {
        if (clicked)
        {
            foreach (KeyCode keycode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keycode))
                {
                    switch(identifier)
                    {
                        case ("shoot"):
                            {
                                shootButton.text = keycode.ToString();
                                clicked = false;
                                PlayerPrefs.SetString("shootKeybind", keycode.ToString());
                                break;
                            }
                        case ("rocket"):
                            rocketButton.text = keycode.ToString();
                            clicked = false;
                            PlayerPrefs.SetString("rocketKeybind",keycode.ToString());
                            break;
                        case ("ability"):
                            abilityButton.text = keycode.ToString();
                            clicked = false;
                            PlayerPrefs.SetString("abilityKeybind", keycode.ToString());
                            break;
                        case ("pickUp"):
                            pickUpButton.text = keycode.ToString();
                            clicked = false;
                            PlayerPrefs.SetString("pickUpKeybind", keycode.ToString());
                            if (GameManager.instance != null)
                            {
                                GameManager.instance.ChangeKeybindText_PickUp();
                            }
                            break;
                    }
                   
                }
            }
        }
    }



    public void ChangeKey()
    {
        switch (identifier)
        {
            case ("shoot"):  
                    shootButton.text = "";
                    break;
            case ("rocket"):
                rocketButton.text = "";
                break;
            case ("ability"):
                abilityButton.text = "";
                break;
            case ("pickUp"):
                pickUpButton.text = "";
                break;
        }
        clicked = true;
    }
}
