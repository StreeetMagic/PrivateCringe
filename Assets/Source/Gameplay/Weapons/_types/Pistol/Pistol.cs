using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class Pistol : Weapon
    {
        public const float ReloadTime = 1;
        public const float MainFireCooldown = .4f;

        private Coroutine _firingCoroutine;
        private Coroutine _reloadingCoroutine;

        public override void Fire(ITargetable target)
        {
            if (_firingCoroutine == null)
            {
                _firingCoroutine = StartCoroutine(Firing(target));
            }
            else
            {
                print("не получается, персонаж уже стреляет");
            }
        }

        public override void Reload(ITargetable target)
        {
            if (_reloadingCoroutine == null)
            {
                _reloadingCoroutine = StartCoroutine(Reloading(target));
            }
            else
            {
                print("не получается, персонаж уже перезаряжает");
            }
        }

        public override void Stop()
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

        private IEnumerator Firing(ITargetable target)
        {
            var pause = new WaitForSeconds(MainFireCooldown);

            while (Magazine.TryFire())
            {
                var bullet = Instantiate(
                    BulletPrefab,
                    ShootingPoint.transform.position,
                    Quaternion.identity);

                bullet.Push(target);

                yield return pause;
            }

            Stop();
            Reload(target);
        }

        private IEnumerator Reloading(ITargetable target)
        {


            yield return new WaitForSeconds(ReloadTime);
            Magazine.Fill(Magazine.MaxCapacity);
            Bullets -= Magazine.MaxCapacity;
            BulletsChanged?.Invoke(Bullets);

            Stop();
            Fire(target);
        }
    }
}