using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : MonoBehaviour
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
        directionMain = direction.normalized*15;
    }


    private void FixedUpdate()
    {
        rb.velocity = directionMain * 2;
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
                rb.AddForceAtPosition(direction.normalized * 50, pos, ForceMode.Force);
            }
        }
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 contactPos = contact.point + contact.normal * 0.1f;
        Instantiate(hitParticles, contactPos, rot);
        Destroy(gameObject);
    }
}
