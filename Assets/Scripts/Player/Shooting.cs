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
        private ObjectPooler bulletPooler;
        private WaitForSeconds waitForSeconds;
        private Coroutine fireBulletCoro;
        private static readonly string Fire1 = "Fire1", gunSound = "GunSound";

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
                bullet.GetComponent<Bullet>().meshRenderer.enabled = true;
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
