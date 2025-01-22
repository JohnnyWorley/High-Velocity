using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPassiveAI : MonoBehaviour
{
    public LayerMask lineCastLayers;
    bool detected = false;
    public GameObject playerTarget;
    private bool rotating;
    public GameObject armature;
    public GameObject pelvisArmature;
    public GameObject spineArmature;
   
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = playerTarget;
    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, Camera.main.transform.position);
        if (!Physics.Linecast(transform.position, Camera.main.transform.position, lineCastLayers) && !detected)
        {
            detected = true;
        }
      
     if (detected)
        {
            // leftArm.transform.LookAt(GetLookAngle());
            // rightArm.transform.LookAt(GetLookAngle());
            Vector3 dirToTarget = player.transform.position - transform.position;
            dirToTarget.y = 0;
            Vector3 dirToTargetNormalized = dirToTarget.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dirToTarget);
            spineArmature.transform.rotation = Quaternion.Lerp(spineArmature.transform.rotation, lookRotation, Time.deltaTime * (10));


            Vector3 dirToTarget2 = player.transform.position - transform.position;
            dirToTarget2.y = 0;
            Vector3 dirToTargetNormalized2 = dirToTarget2.normalized;
            Quaternion lookRotation2 = Quaternion.LookRotation(dirToTargetNormalized2);

            Vector3 pelvisForward = pelvisArmature.transform.TransformDirection(Vector3.forward);

            if (Vector3.Dot(pelvisForward, dirToTargetNormalized2) > -0.6f && Vector3.Dot(pelvisForward, dirToTargetNormalized2) < 0.6f && !rotating)
            {
                rotating = true;
            }
            if (rotating)
            {
                pelvisArmature.transform.rotation = Quaternion.Lerp(pelvisArmature.transform.rotation, (lookRotation2), Time.deltaTime * (720 / 360));
                if (Vector3.Dot(pelvisForward, dirToTargetNormalized2) > 0.99f)
                {
                    rotating = false;
                }
            }
        }
    }

    Vector3 GetLookAngle()
    {
        return new(transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    
}
