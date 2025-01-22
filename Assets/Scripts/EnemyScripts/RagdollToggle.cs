using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class RagdollToggle : MonoBehaviour
{
    [HideInInspector] public bool ragdolled = false;
    [SerializeField] private BoxCollider[] bodyCollider;
    [SerializeField] private GameObject enemyMesh;
    [SerializeField] private GameObject enemyRig;
    private Animator animator;

    void Start()
    {
        //enemyRig = gameObject.;
        bodyCollider = enemyMesh.GetComponentsInChildren<BoxCollider>();
        animator = GetComponent<Animator>();
        GetRagdollParts();
        RagdollOff();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
         if (collision.rigidbody != null)
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude >= 3 && collision.transform.tag != "Bullet")
            {
                RagdollOn();
            }
        }
    }

    Collider[] ragdollColliders;
    [HideInInspector] public Rigidbody[] ragdollRigidbodies;
    void GetRagdollParts()
    {
        ragdollColliders = enemyRig.GetComponentsInChildren<Collider>();
        ragdollRigidbodies = enemyRig.GetComponentsInChildren<Rigidbody>();
        
    }

    
    public void AddBulletForce(Vector3 direction)
    {
        Transform bone = transform.Find("Armature");
        Rigidbody rb = bone.transform.Find("SpineBone").GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direction * 5, ForceMode.Impulse);
        }
    }

    public void RagdollOn()
    {
        transform.tag = "Untagged";
        if (GetComponent<EnemyAI>() != null)
        {
            GetComponent<EnemyAI>().enabled = false;
            GetComponent<EnemyAI>().CancelInvoke();
        }
        else if (GetComponent<EnemyPassiveAI>() != null)
        {
            GetComponent<EnemyPassiveAI>().enabled = false;
        }
        if (GameManager.instance != null)
        {
            GameManager.instance.enemiesKilled += 500;
            if (FirstPersonMovement.instance.grounded == false)
            {
                GameManager.instance.specialMoves += (75 * (int)GameManager.instance.player.GetComponent<Rigidbody>().velocity.magnitude) / 5;
            }
        }
        
        ragdolled = true;
        animator.enabled = false;
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        
        foreach(BoxCollider collider in bodyCollider)
        {
            collider.enabled = false;
        }
    }


   public void RagdollOff()
    {
        ragdolled = false;
        foreach (Collider collider in ragdollColliders) 
        { 
            collider.enabled = false; 
        }

        foreach( Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }

        animator.enabled = true;

        foreach (BoxCollider collider in bodyCollider)
        {
            collider.enabled = true;
        }
    }
}
