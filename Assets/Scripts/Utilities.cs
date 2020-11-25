using UnityEngine;

namespace Utils
{
    public static class Utilities
    {
        public static void SetRandomPosition(this Transform t, float xMin = 0, float xMax = 0, float yMin = 0, float yMax = 0, float zMin = 0, float zMax = 0)
        {
            Random.InitState(50);
            t.localPosition = t.localPosition + new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
        }
    }
}
