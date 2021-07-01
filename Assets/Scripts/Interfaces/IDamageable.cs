using UnityEngine;

namespace Game.Interfaces
{
    public interface IDamageable
    {
        void takeDamage(float damageAmount);

        Transform getTransform();
    }
}
