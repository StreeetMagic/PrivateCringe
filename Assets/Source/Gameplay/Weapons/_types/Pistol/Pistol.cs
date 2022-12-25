using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class Pistol : Weapon
    {
        public override bool TryFire()
        {
            if (_firingCoroutine != null)
            {
                Shooter.Stop();
                return false;
            }

            if (Magazine.Bullets == 0)
            {
                TryReload();
                Shooter.Stop();
                return false;
            }

            _firingCoroutine = StartCoroutine(Firing());

            return true;
        }

        protected override bool TryReload()
        {
            if (_reloadingCoroutine != null)
                return false;

            _reloadingCoroutine = StartCoroutine(Reloading());

            return true;
        }

        protected override IEnumerator Firing()
        {
            var pause = new WaitForSeconds(MainFireCooldown);

            while (Magazine.TryFire())
            {
                FireSingleBullet();

                yield return pause;
            }

            Stop();
            TryReload();
        }

        protected override IEnumerator Reloading()
        {
            Shooter.Stop();
            yield return new WaitForSeconds(ReloadTime);
            Magazine.Fill(Magazine.MaxCapacity);
            Bullets -= Magazine.MaxCapacity;
            BulletsChanged?.Invoke(Bullets);

            Stop();
        }
    }
}