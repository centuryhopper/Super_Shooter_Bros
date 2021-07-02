using System.Collections;
using System.Collections.Generic;
using Game.Interfaces;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider sphereCollider;
    private List<IDamageable> damageables = new List<IDamageable>();
    public float damage = 10;
    public float attackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable target);
    public AttackEvent OnAttack;
    private Coroutine attackCoroutine;
    WaitForSeconds wait;
    [SerializeField] Animator animator = null;
    private const string attack = "attack";

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        wait = new WaitForSeconds(attackDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageables.Add(damageable);

            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            UnityEngine.Debug.Log($"player leaving range");

            damageables.Remove(damageable);
            if (damageables.Count == 0)
            {
                UnityEngine.Debug.LogWarning($"enemy has nothing to attack");
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
            else
            {
                UnityEngine.Debug.Log($"enemy still has something to attack");
            }
        }
    }

    // coroutine for attacking the player
    private IEnumerator Attack()
    {
        yield return wait;

        while (damageables.Count > 0)
        {
            UnityEngine.Debug.Log($"here in attack coroutine");
            IDamageable damageable = null;
            for (var i = 0; i < damageables.Count; i++)
            {
                damageable = damageables[i];

                // play ai attack animation
                OnAttack?.Invoke(damageable);

                damageable?.takeDamage(damage);

            }


            damageable = null;

            yield return wait;

            damageables.RemoveAll(DisabledDamageables);
        }

        attackCoroutine = null;
    }

    private bool DisabledDamageables(IDamageable Damageable)
    {
        return Damageable != null && !Damageable.getTransform().gameObject.activeSelf;
    }
}
