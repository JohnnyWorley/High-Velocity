using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletProjectile : MonoBehaviour
{
    public ParticleSystem particles;
   
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 40;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.instance.Death("Player found out they aren't bulletproof");
        }
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 contactPos = contact.point + contact.normal * 0.1f;
        Instantiate(particles, contactPos, rot);
        Destroy(gameObject);
    }
}
