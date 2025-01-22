using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private float explosionRadius = 4f;
    GameObject player;
    GroundCheck groundCheck;


    void Start()
    {
        groundCheck = FindObjectOfType<GroundCheck>();
        player = GameObject.Find("First Person Controller");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Explosion();
        }
        else if (GetComponent<Rigidbody>().velocity.magnitude >= 10)
        {
            Explosion();
        }
    }


    public void Explosion()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitColliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                 if (rb.CompareTag("Enemy"))
                {
                    RagdollToggle ragdoll = rb.gameObject.GetComponent<RagdollToggle>();
                    ragdoll.RagdollOn();
                    foreach (Rigidbody rigidB in ragdoll.ragdollRigidbodies)
                    {
                        rigidB.AddExplosionForce(240, transform.position, explosionRadius, 0.5f);
                        rigidB.tag = "Untagged";
                    }
                }
                
                 if (rb.CompareTag("Barrel"))
                {
                    rb.transform.GetComponent<ExplosiveBarrel>().Explode();
                }
                else if (rb.CompareTag("Destructable"))
                {
                    Destroy(rb.GetComponent<Rigidbody>());
                    Destroy(rb.GetComponent<BoxCollider>());
                    Transform[] transforms = rb.GetComponentsInChildren<Transform>();
                    foreach (Transform t in transforms)
                    {
                        t.GetComponent<BoxCollider>().enabled = true;
                        if (t.GetComponent<Rigidbody>() != null) {
                            Rigidbody addedRB = t.gameObject.AddComponent<Rigidbody>();
                        }

                        Rigidbody addedRB2 = t.GetComponent<Rigidbody>();
                        t.transform.parent = null;
                        addedRB2.collisionDetectionMode = CollisionDetectionMode.Continuous;
                        if (t.CompareTag("HeavyDebris"))
                        {
                            addedRB2.mass = 7;
                        }
                        else if (t.CompareTag("Screen"))
                        {
                            Destroy(t.gameObject);
                        }
                        else
                        {
                            addedRB2.mass = 1.5f;
                        }
                        t.tag = "Untagged";
                        addedRB2.AddExplosionForce(20, transform.position, explosionRadius, 0.5f, ForceMode.Impulse);
                    }
                }
                else
                {
                    rb.AddExplosionForce(20, transform.position, explosionRadius, 0.5f, ForceMode.VelocityChange);
                }
                   
                Explode();
            }
        }
    }

    public void Explode()
    {
       
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            if (player != null)
            {
               float magnitude = (PlayerPrefs.GetInt("screenShake") * 10 / ((player.transform.position - transform.position).magnitude + 1));
               EZCameraShake.CameraShaker.Instance.ShakeOnce(magnitude, 1000000, 0.15f, 0.7f);
            }
           
            Destroy(gameObject);
        
    }
}
