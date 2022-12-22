using System;
using System.Collections.Generic;
using Gameplay.Humans.Players;
using Gameplay.Interfaces;
using Gameplay.Weapons.Bullets;
using Gameplay.Weapons.Magazines;
using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected bool CanFire;
        [SerializeField] protected Shooter Shooter;

        [field: SerializeField] public int Bullets { get; protected set; }
        [field: SerializeField] public Magazine Magazine { get; protected set; }
        [field: SerializeField] public Transform ShootingPoint { get; protected set; }
        
        public Action<int> BulletsChanged;

        private void Start()
        {
            BulletsChanged?.Invoke(Bullets);
        }

        public void GainBullets(int count)
        {
            if (count > 0)
            {
                Bullets += count;
            }
        }

        public abstract void Fire(ITargetable target);

        public abstract void Reload(ITargetable target);

        public abstract void Stop();
    }
}