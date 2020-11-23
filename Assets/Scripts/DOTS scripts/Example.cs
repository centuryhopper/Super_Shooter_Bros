using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void SimpleExample()
    {

    }

    private struct SimpleJob : IJob
    {
        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
