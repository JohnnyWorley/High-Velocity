using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherManager : MonoBehaviour
{
 ParticleSystem landParticles;
    private void Start()
    {
        landParticles = GetComponentInChildren<ParticleSystem>();
    }
    public void KillEnable()
    {
        GetComponentInChildren<CrusherScript>().ableToKill = true;
    }

    public void CameraShake()
    {
        landParticles.Play();
        GameObject player = GameObject.Find("First Person Controller");
        if ((transform.position - player.transform.position).magnitude < 25)
        {
            EZCameraShake.CameraShaker.Instance.ShakeOnce(15 / (transform.position - player.transform.position).magnitude * 2, 1000000, 0.15f, 0.6f);
        }
    }

    public void KillDisable()
    {
        GetComponentInChildren<CrusherScript>().ableToKill = false;
       GameObject player = GameObject.Find("First Person Controller");       
    }
}
