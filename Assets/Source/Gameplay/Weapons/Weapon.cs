using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet _bulletPrefab;
        [SerializeField] protected Magazine Magazine;
        [SerializeField] protected int Bullets;
        [SerializeField] protected bool CanFire;

        public abstract void Fire();

        public abstract void Reload();

        public void GainBullets(int count)
        {
            if (count > 0)
            {
                Bullets += count;
            }
        }
    }
}