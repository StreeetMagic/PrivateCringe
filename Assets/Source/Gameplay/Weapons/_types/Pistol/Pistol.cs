using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class Pistol : Weapon
    {
        private const float MainFireCooldown = .5f;

        private Coroutine _firingCoroutine;
        private Coroutine _reloadingCoroutine;

        public override void Fire(ITargetable target)
        {
            if (_firingCoroutine == null)
            {
                _firingCoroutine = StartCoroutine(Firing(target));
            }
        }

        public override void Stop()
        {
            if (_firingCoroutine != null)
            {
                StopCoroutine(_firingCoroutine);
                _firingCoroutine = null;
            }
        }

        public override void Reload()
        {
            if (Bullets >= Magazine.MaxCapacity)
            {
                Magazine.Fill(Magazine.MaxCapacity);
                Bullets -= Magazine.MaxCapacity;
                BulletsChanged?.Invoke(Bullets);

                print("Перезарядил");
            }
            else if (Bullets == 0)
            {
                Debug.Log("Нет патронов");

                if (_firingCoroutine != null)
                {
                    StopCoroutine(_firingCoroutine);
                    _firingCoroutine = null;
                }
            }
            else
            {
                var count = Bullets;
                Magazine.Fill(count);
                Bullets -= count;
                BulletsChanged?.Invoke(Bullets);
                print("Перезарядил");
            }
        }

        private IEnumerator Firing(ITargetable target)
        {
            var pause = new WaitForSeconds(MainFireCooldown);

            while (true)
            {
                if (Magazine.TryFire())
                {
                    var bullet = Instantiate(
                        BulletPrefab,
                        ShootingPoint.transform.position,
                        Quaternion.identity);

                    bullet.Push(target);

                    yield return pause;
                }
                else
                {
                    Reload();

                }
            }
        }
    }
}