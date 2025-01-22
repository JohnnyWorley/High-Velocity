using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerScript : MonoBehaviour
{

    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("eee");
            Animator animator = door.GetComponent<Animator>();
            animator.SetTrigger("Door");
            if (door.CompareTag("EndTrigger"))
            {
                door.tag = ("Untagged");
                EndLevel();
            }
           // Destroy(gameObject);
        }
    }



    void EndLevel()
    {
        TimerScript.instance.levelEnded = true;
        StartCoroutine(GameManager.instance.LevelEnd());
    }
}
