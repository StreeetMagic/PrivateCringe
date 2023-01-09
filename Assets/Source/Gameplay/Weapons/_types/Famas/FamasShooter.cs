using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Famas
{
    public class FamasShooter : WeaponShooter
    {
        [SerializeField] private int _fireCount = 3;
        [SerializeField] private float _tripleShotCooldown = 0.06f;
        
        protected override IEnumerator Shooting()
        {
            IsShooting = true;

            while (CanShoot)
            {
                for (int i = 0; i < _fireCount; i++)
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

                        yield return new WaitForSeconds(_tripleShotCooldown);
                    }
                }

                yield return new WaitForSeconds(MainFireCooldown);
            }
        }

    }
}