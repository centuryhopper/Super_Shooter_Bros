using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;
using UnityEditor;

public class DeselectAll : MonoBehaviour
{
    [MenuItem("Custom Shortcuts/Deselect All &a")]
    static void Deselect() => Selection.objects = null;
}
