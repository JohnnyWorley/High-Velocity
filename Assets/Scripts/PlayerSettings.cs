using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using Unity.VisualScripting;
using System;

public class PlayerSettings : MonoBehaviour
{

    public static PlayerSettings instance;

    [Header("Default Settings")]
    int defaultFov = 90;
    int defaultSens = 2;
    int defaultAimSens = 2;
    int defaultAutoSprint = 1;
    int defaultScreenShake = 4;
    string defaultShootKeybind = "Mouse0";
    string defaultRocketKeybind = "E";
    string defaultAbilityKeybind = "Q";
    string defaultPickUpKeybind = "F";



   

    [Header("UI Elements")]
    public Slider fovSlider;
    public Slider sensSlider;
    public Slider aimSensSlider;
    public Toggle autoSprintToggle;
    public Button screenShakeOffButton;
    public Button screenShakeLowButton;
    public Button screenShakeMediumButton;
    public Button screenShakeHighButton;




    [Header("Settings Displays")]
    public TextMeshProUGUI sensDisplay;
    public TextMeshProUGUI aimSensDisplay;
    public TextMeshProUGUI fovDisplay;
    public TextMeshProUGUI shootKeybindText;
    public TextMeshProUGUI rocketKeybindText;
    public TextMeshProUGUI abilityKeybindText;
    public TextMeshProUGUI pickUpKeybindText;


    private void Awake()
    {
        LoadData();
        int hasPlayed = PlayerPrefs.GetInt("HasPlayed");
        if (hasPlayed == 0)
        {
            ResetDefaultKeybinds();
            PlayerPrefs.SetInt("HasPlayed", 1);
            Debug.Log("First Time Player Detected, Resetting Keybinds");
        } 
       SliderValueChange();
    }
    public void ResetDefaultKeybinds()
    {
        fovSlider.value = defaultFov;
        sensSlider.value = defaultSens;
        aimSensSlider.value = defaultAimSens;
        autoSprintToggle.isOn = Convert.ToBoolean(defaultAutoSprint);
        PlayerPrefs.SetInt("screenShake", defaultScreenShake);
        PlayerPrefs.SetString("shootKeybind", defaultShootKeybind);
        PlayerPrefs.SetString("rocketKeybind", defaultRocketKeybind);
        PlayerPrefs.SetString("abilityKeybind", defaultAbilityKeybind);
        PlayerPrefs.SetString("pickUpKeybind", defaultPickUpKeybind);
        ScreenShakeChange();
        SliderValueChange();
        SaveData();
    }

    private void Start()
    {
        instance = this;
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("fov", fovSlider.value);
        PlayerPrefs.SetFloat("sens", sensSlider.value);
        PlayerPrefs.SetFloat("aimSens", aimSensSlider.value);
        PlayerPrefs.SetInt("autoSprint", autoSprintToggle.isOn ? 1 : 0);
        if (GameManager.instance)
        {
            GameManager.instance.ApplyPlayerSettings();
        }
        LoadData();
    }
   
    public void LoadData()
    {
        fovSlider.value = PlayerPrefs.GetFloat("fov");
        sensSlider.value = PlayerPrefs.GetFloat("sens");
        aimSensSlider.value = PlayerPrefs.GetFloat("aimSens");
        autoSprintToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("autoSprint"));
        shootKeybindText.text = PlayerPrefs.GetString("shootKeybind");
        rocketKeybindText.text = PlayerPrefs.GetString("rocketKeybind");
        abilityKeybindText.text = PlayerPrefs.GetString("abilityKeybind");
        pickUpKeybindText.text = PlayerPrefs.GetString("pickUpKeybind");
        ScreenShakeChange();
    }

    public void SliderValueChange()
    {
        sensDisplay.text = sensSlider.value.ToString("F1");
        aimSensDisplay.text = aimSensSlider.value.ToString("F1");
        fovDisplay.text = fovSlider.value.ToString("F0");
    }
    void ScreenShakeChange()
    {
        switch (PlayerPrefs.GetInt("screenShake"))
        {
            case 0:
                screenShakeOffButton.image.color = new Color(0.68f, 0.93f, 0.505f, 1);
                screenShakeLowButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeMediumButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeHighButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);

                break;
            case 2:
                screenShakeOffButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeLowButton.image.color = new Color(0.68f, 0.93f, 0.505f, 1);
                screenShakeMediumButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeHighButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                break;
            case 4:
                screenShakeOffButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeLowButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeMediumButton.image.color = new Color(0.68f, 0.93f, 0.505f, 1);
                screenShakeHighButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                break;
            case 6:
                screenShakeOffButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeLowButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeMediumButton.image.color = new Color(0.93f, 0.505f, 0.505f, 1);
                screenShakeHighButton.image.color = new Color(0.68f, 0.93f, 0.505f, 1);
                break;
        }
    }
}
