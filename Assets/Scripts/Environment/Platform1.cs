using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1 : MonoBehaviour
{

    public Vector3 playerOffset;
    Transform t;



    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // if (t.GetChild(0) != null)
        // {
        //     // offset the player position
        //     Transform playerTransform = t.GetChild(0);

        //     playerTransform.position +=
        // }
    }
}
