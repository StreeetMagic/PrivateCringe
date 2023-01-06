using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Shotgun
{
    public class Shotgun : Weapon
    {
        protected override IEnumerator Firing()
        {
            IsFiring = true;
            var pause = new WaitForSeconds(1);

            while (CanFire)
            {
                if (Magazine.Bullets == 1)
                {
                    Magazine.Clear();

                    if (Bullets > 0)
                    {
                        Reload();
                    }
                    else
                    {
                        Stop();
                    }
                }
                else
                {
                    Magazine.LoseBullet();

                    for (int i = 0; i < 8; i++)
                    {
                        FireSingleBullet();
                    }

                    yield return pause;
                }
            }
        }

        protected override IEnumerator Reloading()
        {
            IsReloading = true;

            yield return new WaitForSeconds(ReloadTime);

            if (Bullets >= Magazine.MaxCapacity)
            {
                Magazine.Fill(Magazine.MaxCapacity);
                Bullets -= Magazine.MaxCapacity;
                BulletsChanged?.Invoke(Bullets);
            }
            else
            {
                Magazine.Fill(Bullets);
                Bullets = 0;
                BulletsChanged?.Invoke(Bullets);
            }

            IsReloading = false;
            StopCoroutine(_reloadingCoroutine);
        }
    }
}