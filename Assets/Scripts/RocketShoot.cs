using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
public class RocketShoot : MonoBehaviour
{
    float cooldown = 3f;
    float cooldownTimer = 0f;
    public GameObject rocket;
    public Image cooldownUI;
    bool rocketFire = true;
    public GameObject rocketSpawn;

    private void Start()
    {
        cooldownUI.fillAmount = 0;
    }

    private IEnumerator rocketEnabler()
    {
        rocketFire = false;
        cooldownTimer = 0f;
        while (cooldownTimer !< cooldown)
        {
            cooldownTimer += Time.deltaTime;
            cooldownUI.fillAmount = 1 - cooldownTimer/cooldown;
            yield return null;
        }
        rocketFire = true;
    }
    void Update()
    {
        KeyCode keycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rocketKeybind"));
        if (Input.GetKeyDown(keycode) && rocketFire && GameManager.instance.isAlive)
        {
            StartCoroutine(rocketEnabler());
            GameObject rocket2 = Instantiate(rocket, rocketSpawn.transform.position, Quaternion.identity);
            rocket2.transform.forward = Camera.main.transform.forward;
        }
    }
}
