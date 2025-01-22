using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    public static Dash instance;


    public LayerMask layerMask;
    public SniperScript sniperScript;
    public Image cooldownUI;
    public Camera renderCamera;
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    Rigidbody rb;
    float cooldown = 2.5f;
    float cooldownTimer = 0f;
    private Camera mainCamera;
    private bool dashReady = true;
    public float playerFov;



    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playerFov = Camera.main.fieldOfView;
        rb= GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        KeyCode abilityKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("abilityKeybind"));
        if (Input.GetKeyDown(abilityKeycode) && dashReady)
        {
            DashForward();
            StartCoroutine(dashEnable());
        }
    }
    IEnumerator dashEnable()
    {
        dashReady = false;
        cooldownTimer = 0f;
        while (cooldownTimer! < cooldown)
        {
            cooldownTimer += Time.deltaTime;
            cooldownUI.fillAmount = 1 - cooldownTimer / cooldown;
            yield return null;
        }
        dashReady = true;
    }
    void DashForward()
    {
        dashDistance = 15;
        Vector3 dashDirection = mainCamera.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dashDirection, out hit, dashDistance, layerMask))
        {
            dashDistance = hit.distance;
        }
        StartCoroutine(PerformDash(dashDirection * dashDistance, dashDuration));
    }

    IEnumerator PerformDash(Vector3 dashVector, float duration)
    {
        sniperScript.scoped = false;
        sniperScript.sniperOverlay.gameObject.SetActive(false);
        renderCamera.gameObject.SetActive(true);
        Camera.main.fieldOfView = 90;
        float timeElapsed = 0f;
        Vector3 originalPosition = transform.position;
        rb.velocity = new Vector3(rb.velocity.x, 0,  rb.velocity.z);


        while (timeElapsed < duration)
        {
            if (Camera.main.fieldOfView <= playerFov + 20)
            {
                renderCamera.fieldOfView += 1;
                Camera.main.fieldOfView += 1;
            }
           
            float step = dashVector.magnitude / duration * Time.deltaTime;
            transform.position += dashVector.normalized * step;
            Rigidbody rb = transform.GetComponent<Rigidbody>();
           //rb.AddForce(Vector3.Reflect(Camera.main.transform.forward, rb.transform.position.normalized);
            timeElapsed += Time.deltaTime;
            yield return null;
           
        }


        float difference = Camera.main.fieldOfView - playerFov;
        for (int i = 0; i < difference; i++)
        {
            renderCamera.fieldOfView -= 1;
            Camera.main.fieldOfView -= 1;
            yield return new WaitForSeconds(0.01f);
        }
        Camera.main.fieldOfView = playerFov;
        renderCamera.fieldOfView = playerFov;
    }
}

