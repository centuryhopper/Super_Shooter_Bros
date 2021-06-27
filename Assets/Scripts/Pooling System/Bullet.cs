using UnityEngine;
using Game.Interfaces;

namespace Game.Pooling
{
    /// <summary>
    /// projectile used by player's weapon
    /// </summary>

    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IPooledObject
    {
        public float bulletForce;
        public float upForce = 1f;
        public float sideForce = .1f;
        public bool shouldCallOnObjectSpawn = false;
        public bool shouldCallOnObjectSpawnWithParam = true;
        public float damageAmount = 10f;

        // public float[] damageAmounst
        // Dictionary of enum values (enemy type) as keys and float values (damageAmount) as values

        public void OnObjectSpawn()
        {
            if (!shouldCallOnObjectSpawn) return;

            float xForce = Random.Range(-sideForce, sideForce);
            float yForce = Random.Range(upForce / 2f, upForce);
            float zForce = Random.Range(-sideForce, sideForce);

            Vector3 force = new Vector3(xForce, yForce, zForce);

            GetComponent<Rigidbody>().velocity = force;
        }

        // fire off to a distance!
        public void OnObjectSpawn(Transform spawnPointTransform)
        {
            if (!shouldCallOnObjectSpawnWithParam) return;

            Rigidbody rb = GetComponent<Rigidbody>();

            // add force in the direction of the fire point position
            rb.AddForce(bulletForce * spawnPointTransform.forward, ForceMode.Impulse);
            // print("fired off into the distance");
        }

        // give the enemy a trigger collider
        // 
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EnemyFighter"))
            {
                // damage enemy
                // UnityEngine.Debug.Log($"shot enemy");

                other.GetComponent<IDamageable>().takeDamage(damageAmount);

                // TODO generate spark effect


            }
        }


        //     #region old plan
        //     //  instantiate bullet contact effect

        //     // put bullet back in the queue

        //     // destroy particle after a couple seconds
        //     #endregion
        // }

































        // public float velocity = 20f;
        // public float lifetime = 1f;

        // int firedByLayer;
        // float lifeTimer;

        // Start is called before the first frame update
        // void Start()
        // {

        // }

        // Update is called once per frame
        // void Update()
        // {
        //     // lock on to target
        //     // if (Physics.Raycast(transform.position, transform.forward, out var hit, velocity * Time.deltaTime, ~(1 << firedByLayer)))
        //     // {
        //     //     transform.position = hit.point;

        //     //     Vector3 reflectedVector = Vector3.Reflect(transform.forward, hit.normal);
        //     //     Vector3 direction = transform.forward;
        //     //     Vector3 vectorOnPlane  = Vector3.ProjectOnPlane(reflectedVector, Vector3.forward);
        //     //     transform.forward = vectorOnPlane;
        //     //     transform.rotation = Quaternion.LookRotation(vectorOnPlane, Vector3.forward);
        //     //     Hit(transform.position, direction, reflectedVector, hit.collider);
        //     // }
        //     // else
        //     // {
        //     //     transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        //     // }

        //     // if (Time.time > lifeTimer + lifetime)
        //     // {
        //     //     // todo put back into queue for object pooling instead of destroying it
        //     //     Destroy(gameObject);
        //     // }
        // }

        // private void Hit(Vector3 position, Vector3 direction, Vector3 reflectedVector, Collider collider)
        // {
        //     Destroy(gameObject);
        // }

        // public void Fire(Vector3 position, Vector3 euler, int layer)
        // {
        //     lifeTimer = Time.time;
        //     transform.position = position;
        //     transform.eulerAngles = euler;
        //     transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        //     Vector3 vectorOnPlane = Vector3.ProjectOnPlane(transform.forward, Vector3.forward);
        //     transform.forward = vectorOnPlane;
        //     transform.rotation = Quaternion.LookRotation(vectorOnPlane, Vector3.forward);
        // }
    }
}
