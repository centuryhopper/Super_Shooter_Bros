using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
// using UnityEngine.Input

public class PointExample : MonoBehaviour
{
    [Header("References"), SerializeField]
    Rig rig = null;

    [Header("Settings"), SerializeField]
    float pointSpeed = 1f;

    int targetValue;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            targetValue = targetValue == 0 ? 1 : 0;
        }

        rig.weight = Mathf.MoveTowards(rig.weight, targetValue, pointSpeed * Time.deltaTime);
    }
}
