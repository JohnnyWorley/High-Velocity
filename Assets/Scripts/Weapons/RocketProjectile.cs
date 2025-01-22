using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class RocketProjectile : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    private float explosionForce = 25f;
    private float explosionRadius = 4f;
    Rigidbody rb;
    GroundCheck groundCheck;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = FindObjectOfType<GroundCheck>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * 30;
    }
    float explosionMultiplier;


    private void OnCollisionEnter(Collision other)
    {

        GameObject player = GameObject.Find("First Person Controller");
        if (groundCheck.isGrounded)
        {
            explosionMultiplier = 1f;
        }
        else
        {
            explosionMultiplier = 1.05f;
        }
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitColliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.CompareTag("Player"))
                {
                     rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                     rb.AddForce(-transform.forward * (explosionForce * explosionMultiplier), ForceMode.Impulse);

                }
               else if (rb.CompareTag("Enemy"))
                {
                    RagdollToggle ragdoll = rb.gameObject.GetComponent<RagdollToggle>();
                    ragdoll.RagdollOn();
                    foreach (Rigidbody rigidB in ragdoll.ragdollRigidbodies)
                    {
                        rigidB.AddExplosionForce(240, transform.position, explosionRadius, 0.5f);
                        rigidB.tag = "Untagged";
                    }
                }
                else if (rb.CompareTag("Barrel"))
                {
                    rb.GetComponent<ExplosiveBarrel>().Explode();
                }
                else if (rb.CompareTag("Destructable"))
                {
                    Destroy(rb.GetComponent<Rigidbody>());
                    Destroy(rb.GetComponent<BoxCollider>());
                    Transform[] transforms = rb.GetComponentsInChildren<Transform>();
                    foreach (Transform t in transforms)
                    {
                        t.GetComponent<BoxCollider>().enabled = true;
                        Rigidbody addedRB = t.gameObject.AddComponent<Rigidbody>();

                        Rigidbody addedRB2 = t.GetComponent<Rigidbody>();
                        t.transform.parent = null;
                        addedRB2.collisionDetectionMode = CollisionDetectionMode.Continuous;
                        if (t.CompareTag("HeavyDebris")) {
                            addedRB2.mass = 7;
                        }
                        else if (t.CompareTag("Screen"))
                        {
                            Destroy(t.gameObject);
                        }
                        else { 
                            addedRB2.mass = 1.5f;
                        }
                        t.tag = "Untagged";
                        addedRB2.AddExplosionForce(20, transform.position, explosionRadius, 0.5f, ForceMode.Impulse);
                    }
                }
               else
                {
                    rb.AddExplosionForce(5, transform.position, explosionRadius, 0.5f, ForceMode.Impulse);
                }
                
            }
        }
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        float magnitude = (PlayerPrefs.GetInt("screenShake") * 10 / ((player.transform.position - transform.position).magnitude + 1));
        EZCameraShake.CameraShaker.Instance.ShakeOnce(magnitude, 1000000, 0.15f, 0.7f);
        Destroy(gameObject);
    }
   

   


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}

    

