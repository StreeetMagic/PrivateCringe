using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Humans.Players;
using Gameplay.Interfaces;
using Gameplay.Weapons.Bullets;
using Gameplay.Weapons.Magazines;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Weapons
{
    [ExecuteAlways]
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] protected float ReloadTime = 1;
        [SerializeField] protected float MainFireCooldown = .4f;
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected Shooter Shooter;
        [SerializeField] protected Reloader Reloader;
        [SerializeField] private Transform[] _aimPoints;

        protected Coroutine _firingCoroutine;
        protected Coroutine _reloadingCoroutine;

        private readonly Random _random = new Random();

        public bool IsReloading { get; protected set; }
        public bool IsFiring { get; protected set; }

        public Action<int> BulletsChanged;
        public Action Reloaded;

        [field: SerializeField] public int Bullets { get; protected set; }
        [field: SerializeField] public Magazine Magazine { get; protected set; }
        [field: SerializeField] public Transform ShootingPoint { get; protected set; }

        public bool CanFire => Magazine.Bullets > 0;
        public bool CanReload => (Bullets > 0 && Magazine.IsEmpty && _reloadingCoroutine == null);

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

        protected void FireSingleBullet()
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

        public void Stop()
        {
            if (IsFiring && _firingCoroutine != null)
            {
                StopCoroutine(_firingCoroutine);
            }

            IsFiring = false;
        }

        public bool TryFire()
        {
            if (IsFiring)
            {
                return false;
            }

            if (IsReloading)
            {
                return false;
            }

            if (Magazine.Bullets < 1)
            {
                return false;
            }

            if (IsFiring == false)
            {
                _firingCoroutine = StartCoroutine(Firing());

                return true;
            }

            return false;
        }

        public void Reload()
        {
            Stop();
            _reloadingCoroutine = StartCoroutine(Reloading());
        }

        protected abstract IEnumerator Firing();

        protected abstract IEnumerator Reloading();
    }
}