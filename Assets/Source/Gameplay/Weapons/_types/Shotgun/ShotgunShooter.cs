using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Shotgun
{
    public class ShotgunShooter : WeaponShooter
    {
        protected override IEnumerator Shooting()
        {
            IsShooting = true;
            var pause = new WaitForSeconds(1);

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

                    for (int i = 0; i < 8; i++)
                    {
                        ShootSingleBullet();
                    }

                    yield return pause;
                }
            }
        }
    }
}