using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class CubeProp : MonoBehaviour
{
    void Start()
    {
        // set random position
        transform.position = new Vector3(Random.Range(-20, 20), Random.Range(0, 50), 0);
    }


}
