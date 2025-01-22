using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PistolScript : MonoBehaviour
{
    public LayerMask layerMask;
    public RawImage crosshair;
    public TextMeshProUGUI ammoText;
    Animator animator;
    public GameObject barrel;
    public Image image;
    public GameObject bullet;
    float nTTFire;
    int fireRate = 4; // bullet per second
    int maxAmmo = 12;
    int ammo;
    [SerializeField]
    bool reloading = false;
    Vector3 preLoadPos;
    private void Start()
    {
        animator = GetComponent<Animator>();
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode shootKeycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("shootKeybind"));
        if (GameManager.instance.isAlive)
        {


            ammoText.text = (ammo + "/" + maxAmmo);
            if (Input.GetKey(shootKeycode) && Time.time >= nTTFire && ammo > 0 && !reloading)
            {
                nTTFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo && !reloading)
            {
                reloading = true;
                preLoadPos = gameObject.transform.position;
                animator.SetTrigger("Reload");
            }

        }
    }



    private void startingEquip()
    {
        reloading = false;
        crosshair.gameObject.SetActive(true);
    }
    private void equipFinished()
    {
    }

    private void Reload()
    {
        reloading = false;
        ammo = maxAmmo;
        gameObject.transform.position = preLoadPos;
    }
    void Shoot()
    {
        animator.SetTrigger("Shoot");
        ammo--;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            targetPoint = hit.point;
        }else
        {
            targetPoint = ray.GetPoint(100);
        }
        PistolProjectile pistolProjectile;
        Vector3 bulletDirection = targetPoint - barrel.transform.position;
        GameObject bullet2 = Instantiate(bullet, barrel.transform.position, Quaternion.identity);
        pistolProjectile = bullet2.GetComponent<PistolProjectile>();
        pistolProjectile.ApplyForward(bulletDirection);
        pistolProjectile.GetDirection(bulletDirection);

    }

    private void OnEnable()
    {
        if (image != null){image.gameObject.SetActive(true);}
    }
    private void OnDisable()
    {
        if (image != null){image.gameObject.SetActive(false);}
    }
}
