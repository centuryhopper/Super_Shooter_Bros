using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Pooling;

namespace Game.PlayerCharacter
{
    public class Shooting : MonoBehaviour
    {
        public Transform firePoint;
        public string bulletTag = "bullet";
        public ParticleSystem muzzleFlash;
        public AudioSource bulletSound;
        public float fireBulletDelay = .1f;
        private ObjectPooler bulletPooler;
        private WaitForSeconds waitForSeconds;
        private Coroutine fireBulletCoro;

        void Awake()
        {
            // initialize bullet gameobject pooler
            bulletPooler = ObjectPooler.Instance;
            waitForSeconds = new WaitForSeconds(fireBulletDelay);
        }

        IEnumerator FireBullet()
        {
            while (true)
            {
                // spawn from pool
                GameObject bullet = bulletPooler.InstantiateFromPool(bulletTag, firePoint);
                muzzleFlash.Play();
                bulletSound.PlayOneShot(bulletSound.clip);
                yield return waitForSeconds;
            }
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                fireBulletCoro = StartCoroutine(FireBullet());
            }

            if (Input.GetButtonUp("Fire1"))
            {
                StopCoroutine(fireBulletCoro);
            }
        }
    }
}
