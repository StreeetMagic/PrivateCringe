using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Famas
{
    public class Famas : Weapon
    {
        [SerializeField] private int _fireCount = 3;
        [SerializeField] private float _tripleShotCooldown = 0.06f;

        public override bool TryFire()
        {
            if (_firingCoroutine != null)
            {
                Shooter.Stop();

                return false;
            }

            if (_reloadingCoroutine != null)
            {
                Shooter.Stop();

                return false;
            }

            if (Magazine.Bullets < _fireCount)
            {
                TryReload();
                Shooter.Stop();

                return false;
            }

            _firingCoroutine = StartCoroutine(Firing());

            return true;
        }

        protected override IEnumerator Firing()
        {
            var singlePause = new WaitForSeconds(_tripleShotCooldown);
            var queuePause = new WaitForSeconds(MainFireCooldown);

            while (Magazine.Bullets >= 1)
            {
                for (int i = 0; i < _fireCount; i++)
                {
                    if (Magazine.TryFire())
                    {
                        FireSingleBullet();

                        yield return singlePause;
                    }
                }

                yield return queuePause;
            }
            Stop();
            TryReload();
        }

        protected override bool TryReload()
        {
            if (_reloadingCoroutine != null)
                return false;

            _reloadingCoroutine = StartCoroutine(Reloading());

            return true;
        }

        protected override IEnumerator Reloading()
        {
            Shooter.Stop();

            yield return new WaitForSeconds(ReloadTime);

            if (Bullets >= Magazine.MaxCapacity)
            {
                Magazine.Fill(Magazine.MaxCapacity);
                Bullets -= Magazine.MaxCapacity;
                BulletsChanged?.Invoke(Bullets);
            }
            else if (Bullets < Magazine.MaxCapacity && Bullets > 0)
            {
                Magazine.Fill(Bullets);
                Bullets = 0;
                BulletsChanged?.Invoke(Bullets);
            }
            else
            {
                print("Нет патронов");
            }

            Stop();
        }
    }
}