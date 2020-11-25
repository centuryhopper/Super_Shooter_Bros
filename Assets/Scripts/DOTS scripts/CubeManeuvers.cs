using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

public class CubeManeuvers : MonoBehaviour
{
    struct OscillatorThread : IJobParallelFor
    {
        // native arrays to hold the positions of the cubes
        public NativeArray<Vector3> positions; // assign outside
        public NativeArray<Vector3> results; // assign outside
        private float x, y, z;
        public float time, frequency, amplitude; // assign outside

        public void Execute(int i)
        {
            // break down into pieces
            x = positions[i].x; y = positions[i].y; z = positions[i].z;

            // (x + our transformation) because we want to offset it
            Vector3 res = new Vector3(x + (amplitude * math.cos(time * frequency)), y, z);

            results[i] = res;
        }

        // calculate the trig result, store in result array,
        // and then assign the cube's transform position to it


    }

    [Range(1,100)]
    public int amplitude = 2;

    [Range(1,100)]
    public float frequency;
    public List<GameObject> cubes;

    [Tooltip("Enable for performance boost")]
    public bool isMultiThreading;

    void Start()
    {
        if (cubes == null)
        {
            int size = 100;
            cubes = new List<GameObject>(size);

            // load cubes
            for (int i = 0; i < size; i++)
            {
                GameObject cube = Resources.Load<GameObject>("Cube");
                if (cube == null)
                {
                    Debug.LogError("please make sure your resources path is correct"); return;
                }
                SetCubePosition(cube, UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10), UnityEngine.Random.Range(0, 10));
                cubes[i] = cube;
            }
        }

    }

    void Update()
    {
        float time = Time.time;

        if (isMultiThreading)
        {
            // store result of the trig calculation
            NativeArray<Vector3> results = new NativeArray<Vector3>(cubes.Count, Allocator.TempJob);

            // get all cube positions
            NativeArray<Vector3> cubePositions = new NativeArray<Vector3>(cubes.Count, Allocator.TempJob);
            for (var i = 0; i < cubes.Count; i++)
            {
                cubePositions[i] = cubes[i].transform.position;
            }

            // create and schedule job
            OscillatorThread parallelThread = new OscillatorThread
            {
                positions = cubePositions,
                time = time,
                frequency = frequency,
                amplitude = amplitude,
                results = results
            };

            JobHandle handle = parallelThread.Schedule(cubes.Count, 1);
            handle.Complete();


            // results array should be populated by now
            // so send that information back to the cubes
            // to update their positions accordingly
            for (var i = 0; i < cubes.Count; i++)
            {
                cubes[i].transform.position = results[i];
            }

            cubePositions.Dispose();
            results.Dispose();
        }
    }

    private void SetCubePosition(GameObject cube, float x, float y, float z)
    {
        cube.transform.position = (Vector3.right * x) + (Vector3.up * y) + (Vector3.forward * z);
    }
}
