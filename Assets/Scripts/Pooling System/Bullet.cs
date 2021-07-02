using UnityEngine;
using Game.Interfaces;
using Game.HealthManager;
using System.Collections;

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
        [SerializeField] bool shouldCallOnObjectSpawn = false;
        [SerializeField] bool shouldCallOnObjectSpawnWithParam = true;
        [SerializeField] bool shouldCallOnObjectSpawnWithPosition = false;
        public string particleTag = "particle";
        ObjectPooler particlePooler = null;

        [HideInInspector]
        public MeshRenderer meshRenderer = null;
        Rigidbody rb = null;
        Coroutine delayForAFrame = null;
        GameObject fleshEffect = null;

        // public float[] damageAmounst
        // Dictionary of enum values (enemy type) as keys and float values (damageAmount) as values

        void Awake()
        {
            particlePooler = ObjectPooler.Instance;
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            rb = GetComponent<Rigidbody>();
            fleshEffect = Resources.Load<GameObject>("BulletImpactFleshSmallEffect");
        }

        public void stopPreviousCoroutine()
        {
            if (delayForAFrame != null) StopCoroutine(delayForAFrame);
        }

        public void OnObjectSpawn()
        {
            if (!shouldCallOnObjectSpawn) return;

            float xForce = Random.Range(-sideForce, sideForce);
            float yForce = Random.Range(upForce / 2f, upForce);
            float zForce = Random.Range(-sideForce, sideForce);

            Vector3 force = new Vector3(xForce, yForce, zForce);

            rb.velocity = force;
        }

        // fire off to a distance!
        public void OnObjectSpawn(Transform spawnPointTransform)
        {
            if (!shouldCallOnObjectSpawnWithParam) return;

            // add force in the direction of the fire point position
            rb.AddForce(bulletForce * spawnPointTransform.forward, ForceMode.Impulse);
            // print("fired off into the distance");
        }

        IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.localPosition, Vector3.one / 6f);
            // Gizmos.color = Color.magenta;
            // Gizmos.DrawLine(transform.localPosition, transform.forward);
        }

        void Update()
        {
            Debug.DrawRay(transform.localPosition, transform.forward*1.5f, Color.red);
        }

        // give the enemy a trigger collider
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                // damage enemy
                other.GetComponent<IDamageable>()?.takeDamage(HealthDamageManager.instance.enemyDamageAmount);

                // collide with anything except the bullet itself and the player
                if (Physics.Raycast(transform.localPosition, transform.forward*1.5f, out RaycastHit hit))
                {
                    // UnityEngine.Debug.Log($"hit the enemy at {hit.point}");
                    GameObject obj = Instantiate(fleshEffect, hit.point, Quaternion.identity);
                    ParticleSystem fleshHitEffects = obj.GetComponent<ParticleSystem>();
                    fleshHitEffects.Play();
                    Destroy(obj, 1.5f);

                    #region I tried to optimize the particle system for reusing instead of destroying but I failed
                    // var fleshEffect = particlePooler.InstantiateFromPool(particleTag, hit.point);
                    // // fleshEffect.GetComponent<ParticleSystem>().Play();
                    // // stopPreviousCoroutine();
                    // // delayForAFrame = StartCoroutine(delay());
                    // // fleshEffect.SetActive(false);
                    #endregion
                    
                }
                else
                {
                    // UnityEngine.Debug.Log($"layername: {LayerMask.LayerToName(7)}");
                    UnityEngine.Debug.Log($"didnt hit anything");
                }

                // stop the velocity and move somewhere outside of the game area
                // rb.velocity = Vector3.zero;
                // transform.position = Vector3.zero;

                this.gameObject.SetActive(false);
                // meshRenderer.enabled = false;
            }
        }

        void ProcessTriggerCollision()
        {

        }

        public void OnObjectSpawn(Vector3 position)
        {
            if (!shouldCallOnObjectSpawnWithPosition) return;

            throw new System.NotImplementedException();
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
