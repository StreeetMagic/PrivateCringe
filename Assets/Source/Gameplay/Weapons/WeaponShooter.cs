using System;
using System.Collections;
using Gameplay.Weapons.Bullets;
using Gameplay.Weapons.Magazines;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Weapons
{
    [ExecuteAlways]
    public abstract class WeaponShooter : MonoBehaviour
    {
        protected const float MainFireCooldown = .4f;

        [field: SerializeField] public Weapon Weapon { get; private set; }

        [SerializeField] protected Bullet BulletPrefab;

        protected WeaponBandolier WeaponBandolier => Weapon.WeaponBandolier;
        protected WeaponMagazine WeaponMagazine => Weapon.WeaponMagazine;
        protected WeaponReloader WeaponReloader => Weapon.WeaponReloader;

        [SerializeField] private Transform[] _aimPoints;

        protected Coroutine _shootingCoroutine;

        public bool CanShoot => WeaponMagazine.Bullets > 0;
        public bool IsShooting { get; protected set; }

        protected abstract IEnumerator Shooting();

        private readonly Random _random = new Random();

        [field: SerializeField] public Transform ShootingPoint { get; protected set; }

        private void Update()
        {
            foreach (var aimPoint in _aimPoints)
            {
                Debug.DrawRay(
                    ShootingPoint.position,
                    (aimPoint.position - ShootingPoint.position) * 100,
                    Color.cyan);
            }
        }

        private void OnEnable()
        {
            Stop();
        }

        public void Stop()
        {
            if (IsShooting && _shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
            }

            IsShooting = false;
        }

        protected void ShootSingleBullet()
        {
            var bullet = Instantiate(
                BulletPrefab,
                ShootingPoint.transform.position,
                Quaternion.identity);

            var randomNumber = _random.Next(_aimPoints.Length);
            var randomAimPoint = _aimPoints[randomNumber];
            var direction = (randomAimPoint.position - ShootingPoint.position);

            bullet.Push(direction);
        }

        public bool TryShoot()
        {
            if (IsShooting)
                return false;

            if (WeaponReloader.IsReloading)
                return false;

            if (WeaponMagazine.Bullets < 1)
                return false;

            if (IsShooting == false)
            {
                _shootingCoroutine = StartCoroutine(Shooting());

                return true;
            }

            return false;
        }
    }
}