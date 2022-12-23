using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Humans.Players;
using Gameplay.Interfaces;
using Gameplay.Weapons.Bullets;
using Gameplay.Weapons.Magazines;
using UnityEngine;

namespace Gameplay.Weapons
{
    [ExecuteAlways]
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected float ReloadTime = 1;
        [SerializeField] protected float MainFireCooldown = .4f;
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected Shooter Shooter;

        protected Coroutine _firingCoroutine;
        protected Coroutine _reloadingCoroutine;

        [field: SerializeField] public int Bullets { get; protected set; }
        [field: SerializeField] public Magazine Magazine { get; protected set; }
        [field: SerializeField] public Transform ShootingPoint { get; protected set; }

        [field: SerializeField] public Transform ShootingPoint1 { get; protected set; }
        [field: SerializeField] public Transform ShootingPoint2 { get; protected set; }
        [field: SerializeField] public Transform ShootingPoint3 { get; protected set; }

        public Action<int> BulletsChanged;

        private void Update()
        {
            Debug.DrawRay(ShootingPoint.position, (ShootingPoint1.position - ShootingPoint.position) * 100, Color.cyan);
            Debug.DrawRay(ShootingPoint.position, (ShootingPoint2.position - ShootingPoint.position) * 100, Color.cyan);
            Debug.DrawRay(ShootingPoint.position, (ShootingPoint3.position - ShootingPoint.position) * 100, Color.cyan);
        }

        private void Start()
        {
            BulletsChanged?.Invoke(Bullets);
        }

        protected void GainBullets(int count)
        {
            if (count > 0)
            {
                Bullets += count;
            }
        }

        protected void FireSingleBullet(ITargetable target)
        {
            var bullet = Instantiate(
                BulletPrefab,
                ShootingPoint.transform.position,
                Quaternion.identity);

            bullet.Push(target);
        }

        public void Stop()
        {
            if (_firingCoroutine != null)
            {
                StopCoroutine(_firingCoroutine);
                _firingCoroutine = null;
            }

            if (_reloadingCoroutine != null)
            {
                StopCoroutine(_reloadingCoroutine);
                _reloadingCoroutine = null;
            }
        }

        public abstract bool TryFire(ITargetable target);

        protected abstract IEnumerator Firing(ITargetable target);

        protected abstract bool TryReload();

        protected abstract IEnumerator Reloading();
    }
}