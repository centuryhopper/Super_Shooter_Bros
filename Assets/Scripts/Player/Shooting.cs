using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Pooling;
using Game.Audio;

namespace Game.PlayerCharacter
{
    public class Shooting : MonoBehaviour
    {
        public Transform firePoint;
        public string bulletTag = "bullet";
        public ParticleSystem muzzleFlash;
        public float fireBulletDelay = .1f;
        public int numBullets = 100;
        private ObjectPooler bulletPooler;
        ObjectPool bulletPool = null;
        private WaitForSeconds waitForSeconds;
        private Coroutine fireBulletCoro;
        private const string Fire1 = "Fire1", gunSound = "GunSound";

        void Awake()
        {
            // initialize bullet gameobject pooler
            // bulletPooler = ObjectPooler.Instance;
            waitForSeconds = new WaitForSeconds(fireBulletDelay);
            bulletPool = ObjectPool.CreateInstance(Resources.Load<Bullet>("Bullet"), numBullets);
        }

        IEnumerator FireBullet()
        {
            while (true)
            {
                // spawn from pool
                PoolableObject bullet = bulletPool.GetAndSetObject(firePoint);
                if (bullet == null)
                {
                    UnityEngine.Debug.LogWarning($"Out of bullets");
                    // stop the entire coroutine
                    yield break;
                }

                // make sure the bullet comes out of the gun fire point
                // bullet.transform.SetParent(firePoint);
                // bullet.transform.localPosition = firePoint.position;

                // show gun shooting spark and sound effect
                muzzleFlash.Play();
                AudioManager.instance.Play(gunSound, 1);
                yield return waitForSeconds;
            }
        }

        public void StopAllShootingCoroutines() => StopAllCoroutines();

        void Update()
        {
            if (Input.GetButtonDown(Fire1))
            {
                if (fireBulletCoro != null) StopCoroutine(fireBulletCoro);
                fireBulletCoro = StartCoroutine(FireBullet());
            }

            if (Input.GetButtonUp(Fire1) && fireBulletCoro != null)
            {
                StopCoroutine(fireBulletCoro);
            }
        }
    }
}
