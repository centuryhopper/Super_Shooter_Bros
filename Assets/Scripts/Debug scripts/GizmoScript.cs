using UnityEngine;

/// <summary>
/// Note: You cannot dynamically change a gizmo's color in Unity
/// </summary>
public class GizmoScript : MonoBehaviour
{
    // [SerializeField] float x = 40, y = 50, z = 0;
    [SerializeField] float playerAimRadius = 5f;

    void OnDrawGizmosSelected()
    {
        // Gizmos.DrawWireCube(transform.position + new Vector3(0, y, 0), new Vector3(x, y * 2, z));
        // print("called OnDrawGizmos");
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerAimRadius);
    }
}
