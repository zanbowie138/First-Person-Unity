using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDummy : MonoBehaviour
{
    public GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")){
            Instantiate(prefab, this.transform.position, Quaternion.identity);
        }
    }
}
