using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Shotgun
{
    public class ShotgunShooter : Shooter
    {
        protected override IEnumerator Shooting()
        {
            IsShooting = true;
            var pause = new WaitForSeconds(1);

            while (CanShoot)
            {
                if (Magazine.Bullets == 1)
                {
                    Magazine.Clear();

                    if (Bandolier.Bullets > 0)
                    {
                        
                        Reloader.Reload();
                    }
                    else
                    {
                        StopShooting();
                    }
                }
                else
                {
                    Magazine.LoseBullets(1);

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