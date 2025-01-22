using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PistolProjectile : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trailRenderer;
    public ParticleSystem hitParticles;
    [SerializeField] private Vector3 directionMain;
    ParticleDestroy ParticleDestroy;
    void Start()
    {
        ParticleDestroy = GetComponentInChildren<ParticleDestroy>();
        rb = GetComponent<Rigidbody>();
    }

    public void GetDirection(Vector3 direction)
    {
        hitParticles.transform.forward = direction;
    }
    public void ApplyForward(Vector3 direction)
    {
        directionMain = direction.normalized;
        transform.forward = directionMain;
    }
    private void FixedUpdate()
    {
        rb.velocity = directionMain * 75;
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.transform.tag = ("Untagged");
                collision.transform.GetComponent<RagdollToggle>().RagdollOn();
                collision.transform.GetComponent<RagdollToggle>().AddBulletForce(transform.forward);
            }
            else
            {
                ParticleDestroy.enabled = true;
                Vector3 pos = transform.position;
                Vector3 direction = transform.position - collision.transform.position;
                rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForceAtPosition(direction.normalized * 5, pos, ForceMode.Acceleration);
            }       
        }
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 contactPos = contact.point + contact.normal * 0.05f;
        Instantiate(hitParticles, contactPos, rot);
        Destroy(gameObject);
    }
}

