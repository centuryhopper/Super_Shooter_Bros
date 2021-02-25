using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalOscillate : MonoBehaviour
{
    Transform t;
    [SerializeField, Range(0,5f), Tooltip("The range of oscillation")] float amplitude;
    [SerializeField, Range(0,25f), Tooltip("How fast the platform will oscillate")] float frequency;
    [SerializeField, Range(0,100f)] float offset = 5f;

    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        float x = t.position.x;
        float y = t.position.y;
        float z = t.position.z;
        z = offset + amplitude * Mathf.Cos(frequency * Time.timeSinceLevelLoad);
        t.position = new Vector3(x, y, z);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.transform.CompareTag("Player"))
        {
            collisionInfo.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.transform.CompareTag("Player"))
        {
            collisionInfo.transform.SetParent(null);
        }
    }



}
