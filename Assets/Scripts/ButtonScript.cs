using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    static int buttonsPressed;
    public GameObject door;
    public GameObject buttonDisplayLight;
    public Material offMaterial;
    public Material onMaterial;
    Renderer lightRenderer;
    Renderer buttonRenderer;
    private void Start()
    {
        buttonsPressed = 0;
        buttonRenderer = GetComponent<Renderer>();
        lightRenderer = buttonDisplayLight.GetComponent<Renderer>();
        lightRenderer.material = offMaterial;
        buttonRenderer.material = offMaterial;
    }
    public void ButtonPress()
    {
        transform.tag = "Untagged";
        buttonsPressed++;
        lightRenderer.material = onMaterial;
        buttonRenderer.material = onMaterial;
        if (buttonsPressed >= 3) 
        {
            door.GetComponent<Animator>().SetTrigger("DoorOpen");
        }
    }
}
