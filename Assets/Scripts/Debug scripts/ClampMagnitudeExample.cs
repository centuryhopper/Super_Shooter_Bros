using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampMagnitudeExample : MonoBehaviour
{
    // Move the object around with the arrow keys but confine it
    // to a given radius around a center point.

    public Vector3 centerPt;
    public float radius;

    void Update()
    {
        // Get the new position for the object.
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 newPos = transform.position + movement;

        // Calculate the distance of the new position from the center point. Keep the direction
        // the same but clamp the length to the specified radius.
        Vector3 offset = newPos - centerPt;
        transform.position = centerPt + Vector3.ClampMagnitude(offset, radius);
    }
}
