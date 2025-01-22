using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIColourChanger : MonoBehaviour
{
    public int magnitudeChangeAmount;
    public int screenShakeSelected;
    public TextMeshProUGUI parentText;
    Button[] optionButtons;
    private Button button;
    private void Awake()
    {
        optionButtons = parentText.GetComponentsInChildren<Button>();
        button = this.GetComponent<Button>();
    }
    public void ChangeUIColour()
    {
       foreach (Button button in optionButtons)
        {
            button.image.color = new Color(0.93f, 0.505f, 0.505f, 1); // off colour
        }
        PlayerPrefs.SetInt("screenShake", screenShakeSelected);
        button.image.color = new Color(0.68f, 0.93f, 0.505f, 1); // on colour
    }

}
