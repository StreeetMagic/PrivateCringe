using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected bool CanFire;

        [field: SerializeField] public int Bullets { get; protected set; }
        [field: SerializeField] public Magazine Magazine { get; protected set; }
        [field: SerializeField] public Transform ShootingPoint { get; protected set; }
        
        public Action<int> BulletsChanged;

        private void OnEnable()
        {
            BulletsChanged?.Invoke(Bullets);
        }

        public abstract void Fire(Transform target);

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