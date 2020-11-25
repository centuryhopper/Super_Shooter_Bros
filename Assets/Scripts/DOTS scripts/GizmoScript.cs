using UnityEngine;

public class GizmoScript : MonoBehaviour
{
    public float x = 40, y = 50, z = 0;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0, y, 0), new Vector3(x, y * 2, z));
    }
}
