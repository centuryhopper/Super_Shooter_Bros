using UnityEngine;

namespace Game.Plaforms
{
    public class HorizontalOscillate : MonoBehaviour
    {
        Transform t;
        float startingZ;
        [SerializeField, Range(0, 5f), Tooltip("The range of oscillation")] float amplitude;
        [SerializeField, Range(0, 25f), Tooltip("How fast the platform will oscillate")] float frequency;
        [SerializeField, Range(0, 10f)] float offset = 5f;


        // Start is called before the first frame update
        void Start()
        {
            t = transform;
            startingZ = t.position.z;
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

            // properly offset the correctly axis for good oscillation
            z = startingZ + offset + amplitude * Mathf.Cos(frequency * Time.timeSinceLevelLoad);
            t.position = new Vector3(x, y, z);
        }

        void OnCollisionEnter(Collision collisionInfo)
        {
            if (collisionInfo.transform.CompareTag("Player"))
            {
                // set player parent to this platorm so that the player will be moved
                // by the platform it is currently on
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
}
