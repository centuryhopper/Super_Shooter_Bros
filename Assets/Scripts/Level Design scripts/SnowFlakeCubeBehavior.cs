using UnityEngine;

public class SnowFlakeCubeBehavior : MonoBehaviour
{
    [Range(100, 1000)]
    public float rotateSpeed = 400f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateSpeed * new Vector3(0,1,0) * Time.deltaTime);
    }
}
