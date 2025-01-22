using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OnScreenTexChanger : MonoBehaviour
{
    public int indentifier;
    public static OnScreenTexChanger tutorialTextChanger;
    public string tutorialText;
    public TextMeshProUGUI tutorialUI;
    float uiHideTimer = 4f;

    void Start()
    {
        tutorialTextChanger = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (indentifier)
            {
                case 0:
                    tutorialUI.text = "WASD to move and Mouse to look around";
                   StartCoroutine(BackgroundTimer());
                    break;
                case 1:
                    tutorialUI.text = "Press " + PlayerPrefs.GetString("rocketKeybind") + " to shoot a rocket and move the boxes";
                    StartCoroutine(BackgroundTimer());
                    break;
                case 2:
                    tutorialUI.text = "You can also use the rocket to propel yourself upwards";
                    StartCoroutine(BackgroundTimer());
                    break;
                case 3:
                    tutorialUI.text = "Rocket Jump to reach the end";
                    StartCoroutine(BackgroundTimer());
                    break;
                case 4:
                    tutorialUI.text = "Tip: Jumping and using your rocket can increase your vertical by up to 15%";
                    StartCoroutine(BackgroundTimer());
                    break;
                case 5:
                    tutorialUI.text = "Stay (sic)";
                    StartCoroutine(BackgroundTimer());
                    break;
            }
        }
      //  gameObject.GetComponent<BoxCollider>().enabled = false;
    }

   public IEnumerator BackgroundTimer()
    {
        uiHideTimer = 4;
        tutorialUI.transform.parent.gameObject.SetActive(true);
        while (uiHideTimer > 0)
        {
            uiHideTimer -= Time.deltaTime;
            tutorialUI.transform.parent.gameObject.SetActive(true);
            yield return null;
        }
        yield return false;
        tutorialUI.transform.parent.gameObject.SetActive(false);

    }

}  

   


