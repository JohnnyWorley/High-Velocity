using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonMovement : MonoBehaviour
{
    bool critcalSpeed = false;
    public static FirstPersonMovement instance;
    GroundCheck groundCheck;
    public bool grounded;
    public ParticleSystem speedLines;
    public float speed = 5;
    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    Rigidbody rb;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    Vector3 hitPoint;



    private void Start()
    {
    }
    void Awake()
    {
        instance = this;
        groundCheck = GetComponentInChildren<GroundCheck>();
        //GetComponentInChildren<MeshRenderer>().enabled = false;

        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
            // Update IsRunning from input.
            IsRunning = canRun && Input.GetKey(runningKey);

            // Get targetMovingSpeed.
            float targetMovingSpeed = IsRunning ? runSpeed : speed;
            if (speedOverrides.Count > 0)
            {
                targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
            }

            // Get targetVelocity from input.
            Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

            // Apply movement.
            rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
        
        
    }



    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 0.1f, Color.red, 200f);
        grounded = groundCheck.isGrounded;
       
        if (rb.velocity.magnitude >= 20)
        {
            critcalSpeed = true;
            speedLines.gameObject.SetActive(true);
        }
        else if (groundCheck.isGrounded == true)
        {
            if (critcalSpeed)
            {
                EZCameraShake.CameraShaker.Instance.ShakeOnce(4, 1000000, 0.15f, 0.2f);
                critcalSpeed = false;
            }
            speedLines.gameObject.SetActive(false);
        }
    }
}


    

