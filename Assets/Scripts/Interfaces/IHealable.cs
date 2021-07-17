using UnityEngine;

namespace Game.Interfaces
{
    public interface IHealable
    {
        void gainHealth(float healthAmount);
        Transform getTransform();
    }
}
