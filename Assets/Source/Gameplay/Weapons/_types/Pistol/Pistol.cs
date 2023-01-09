using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class Pistol : Weapon
    {
       
        protected override IEnumerator Firing()
        {
            IsFiring = true;
            
            var pause = new WaitForSeconds(MainFireCooldown);

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
                    FireSingleBullet();

                    yield return pause;
                }
            }
            IsFiring = false;
        }

        protected override IEnumerator Reloading()
        {
            IsReloading = true;
            yield return new WaitForSeconds(ReloadTime);
            Magazine.Fill(Magazine.MaxCapacity);
            Bullets -= Magazine.MaxCapacity;
            BulletsChanged?.Invoke(Bullets);
            IsReloading = false;
        }
    }
}