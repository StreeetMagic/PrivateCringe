using System;
using System.Collections;
using Gameplay.Weapons.Bullets;
using Gameplay.Weapons.Magazines;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Weapons
{
    [ExecuteAlways]
    public abstract class Shooter : MonoBehaviour
    {
        protected const float MainFireCooldown = .4f;

        [SerializeField] protected int _shootQueue = 1;
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] private Transform[] _aimPoints;
        protected Coroutine _shootingCoroutine;

        [field: SerializeField] public Weapon Weapon { get; private set; }
        public bool CanShoot => Weapon.Magazine.Bullets > _shootQueue;
        public bool IsShooting { get; protected set; }
        private readonly Random _random = new Random();

        protected abstract IEnumerator Shooting();


        [field: SerializeField] public Transform ShootingPoint { get; protected set; }

        private void Update()
        {
            foreach (var aimPoint in _aimPoints)
            {
                var position = ShootingPoint.position;
                
                Debug.DrawRay(
                    position,
                    (aimPoint.position - position) * 100,
                    Color.cyan);
            }
        }

        private void OnEnable()
        {
            StopShooting();
        }

        public void StopShooting()
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
            var direction = randomAimPoint.position - ShootingPoint.position;

            bullet.Push(direction);
        }

        public bool TryShoot()
        {
            if (IsShooting)
                return false;

            if (Weapon.Reloader.IsReloading)
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