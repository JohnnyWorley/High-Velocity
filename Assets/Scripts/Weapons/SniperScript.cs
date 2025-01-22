using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;





public class SniperScript : MonoBehaviour
{
    public Image image;
    public ParticleSystem hitParticles;
    public LayerMask layerMask;
    public GameObject player;
    public RawImage crosshair;
    public TextMeshProUGUI ammoText;
    public bool scoped = false;
    Animator animator;
    int maxAmmo = 1;
    int ammo;
    bool reloading = false;
    Vector3 preLoadPos;
    public Image sniperOverlay;
    public GameObject weaponCamera;
    FirstPersonLook FPL;
    public static SniperScript instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        FPL = FindObjectOfType<FirstPersonLook>();
        animator = GetComponent<Animator>();
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode shootKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootKeybind"));



        if (GameManager.instance.isAlive)
        {
            if (scoped)
            {
                FirstPersonLook.instance.sensitivity = PlayerPrefs.GetFloat("aimSens");    
            }
            else
            {
                FirstPersonLook.instance.sensitivity = PlayerPrefs.GetFloat("sens");
            }



            ammoText.text = (ammo + "/" + maxAmmo);
            if (Input.GetKeyDown(shootKeycode) && ammo > 0)
            {
                Shoot();
            }

            else if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo && !reloading)
            {
                Camera.main.fieldOfView = PlayerPrefs.GetFloat("fov");
                weaponCamera.SetActive(true);
                sniperOverlay.gameObject.SetActive(false);
                scoped = false;
                reloading = true;
                animator.SetTrigger("Reload");

            }

            else if (Input.GetKeyDown(KeyCode.Mouse1) && !reloading)
            {
                if (!scoped)
                {
                    Scope();
                }
                else if (scoped)
                {
                    UnScope();
                }
            }
        }
    }

    public void Scope()
    {
        Camera.main.fieldOfView = 15f;
        weaponCamera.SetActive(false);
        sniperOverlay.gameObject.SetActive(true);
        scoped = true;
    }

    public void UnScope()
    {
        Camera.main.fieldOfView = PlayerPrefs.GetFloat("fov");
        weaponCamera.SetActive(true);
        sniperOverlay.gameObject.SetActive(false);
        scoped = false;
    }

    private void Reload()
    {
        reloading = false;
        ammo = maxAmmo;

    }

    private void startingEquip()
    {
        reloading = false;
        crosshair.gameObject.SetActive(false);
    }
    private void equipFinished()
    {
    }

    void Shoot()
    {
        
        ammo--;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.rigidbody != null)
            {
                Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                rb.AddForceAtPosition(Camera.main.transform.forward * 300, hit.point, ForceMode.Force);

            }



            if (hit.transform.CompareTag("Enemy"))
            {
                if (hit.transform.GetComponent<RagdollToggle>() != null)
                {
                    hit.transform.GetComponent<RagdollToggle>().RagdollOn();
                    //hit.transform.GetComponent<RagdollToggle>().AddBulletForce(Camera.main.transform.forward);
                }
            }
            else if (hit.transform.CompareTag("Barrel"))
            {
                hit.transform.GetComponent<ExplosiveBarrel>().Explosion();
            }
            else if (hit.transform.CompareTag("Glass"))
            {
                hit.transform.GetComponent<BreakWindow>().WindowBreakFunction();
            }

            Vector3 contactPos = hit.point;
            Instantiate(hitParticles, contactPos, Quaternion.FromToRotation(Vector3.up, hit.normal + hit.normal * 0.1f));
        }
        animator.SetTrigger("Shoot");
        if (scoped)
        {
            Camera.main.fieldOfView = PlayerPrefs.GetFloat("fov");
            weaponCamera.SetActive(true);
            sniperOverlay.gameObject.SetActive(false);
            scoped = false;
        }
    }


    private void OnEnable()
    {
        image.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        FirstPersonLook.instance.sensitivity = PlayerPrefs.GetFloat("sens");
        if (image){image.gameObject.SetActive(false);}
    }

}
