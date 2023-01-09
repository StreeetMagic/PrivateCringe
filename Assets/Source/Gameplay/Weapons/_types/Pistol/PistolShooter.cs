using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class PistolShooter : WeaponShooter
    {       
        protected override IEnumerator Shooting()
        {
            IsShooting = true;
            
            var pause = new WaitForSeconds(MainFireCooldown);

            while (CanShoot)
            {
                if (WeaponMagazine.Bullets == 1)
                {
                    WeaponMagazine.Clear();

                    if (WeaponBandolier.Bullets > 0)
                    {
                        WeaponReloader.Reload();
                    }
                    else
                    {
                        Stop();
                    }
                }
                else
                {
                    WeaponMagazine.LoseBullet();
                    ShootSingleBullet();

                    yield return pause;
                }
            }
            IsShooting = false;
        }

    }
}