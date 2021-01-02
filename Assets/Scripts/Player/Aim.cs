using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    Animator animator;

    Transform chest;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        chest = animator.GetBoneTransform(HumanBodyBones.Chest);
    }

    void LateUpdate()
    {
        chest.LookAt(target.position);
        chest.rotation *= Quaternion.Euler(offset);
    }
}
