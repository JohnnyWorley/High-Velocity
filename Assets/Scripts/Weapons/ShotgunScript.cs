using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : MonoBehaviour
{
    public Image image;
    private int pelletAmount = 7;
    public LayerMask layerMask;
    public RawImage crosshair;
    public TextMeshProUGUI ammoText;
    Animator animator;
    public GameObject barrel;
    ShotgunProjectile shotgunProjectile;
    public GameObject bullet;
    float nTTFire;
    int maxAmmo = 4;
    int ammo;
    bool reloading = false;
    bool continueReloading = false;
    bool readyToFire = true;
    Vector3 preLoadPos;
    private void Start()
    {
        animator = GetComponent<Animator>();
        ammo = maxAmmo;
    }
    private void Awake()
    {
        readyToFire = true;
    }
    // Update is called once per frame
    void Update()
    {
        KeyCode shootKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootKeybind"));
        if (GameManager.instance.isAlive)
        {
            ammoText.text = (ammo + "/" + maxAmmo);
            if (Input.GetKeyDown(shootKeycode) && ammo > 0 && !reloading && readyToFire)
            {
                continueReloading = false;
                ammo--;
                readyToFire = false;
                animator.SetTrigger("Shoot");
                for (int i = 0; i < pelletAmount; i++)
                {
                    Shoot();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo && !continueReloading)
            {

                if (continueReloading == false)
                {
                    continueReloading = true;
                }
                reloading = true;
                preLoadPos = gameObject.transform.position;
                animator.SetTrigger("Reload");
            }
        }
    }


    private void startingEquip()
    {
        continueReloading = false;
        reloading = false;
        crosshair.gameObject.SetActive(true);
        readyToFire = true;
    }




    private void Reload()
    {
        ammo++;
         gameObject.transform.position = preLoadPos;
        if (continueReloading == true && ammo < maxAmmo)
        {
            animator.SetTrigger("Reload");
        }
        reloading = false;
    }

    void finishShooting()
    {
        readyToFire = true;
    }












    void Shoot()
    {
        float spreadX = Random.Range(0.49f, 0.51f);
        float spreadY = Random.Range(0.48f, 0.52f);

        // animator.SetTrigger("Shoot");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(spreadX, spreadY, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit,Mathf.Infinity, layerMask))
        {
            targetPoint = hit.point;
        }

        else
        {
            targetPoint = ray.GetPoint(1000);
        }
        Vector3 bulletDirection = (targetPoint - barrel.transform.position);
        GameObject bullet2 = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        shotgunProjectile = bullet2.GetComponent<ShotgunProjectile>();
        bullet2.transform.forward = bulletDirection;
        shotgunProjectile.GetDirection(bulletDirection);
    }

    private void OnEnable()
    {
        if (image != null)
        {
            image.gameObject.SetActive(true);
        }
    }
    private void OnDisable()
    {
        if (image != null)
        {
            image.gameObject.SetActive(false);
        }
    }
}
