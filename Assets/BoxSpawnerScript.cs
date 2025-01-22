using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerScript : MonoBehaviour
{
    public GameObject box;
    bool canSpawn = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(BoxSpawn());
        }
    }

    IEnumerator BoxSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(Random.Range(3, 4));
        Instantiate(box, transform.position, new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        canSpawn = true;
    }
}
