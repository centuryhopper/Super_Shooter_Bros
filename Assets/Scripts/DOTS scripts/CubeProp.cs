using UnityEngine;

public class CubeProp : MonoBehaviour
{
    void Start()
    {
        // set random position
        transform.position = new Vector3(Random.Range(-20, 20), Random.Range(0, 50), 0);
    }


}
