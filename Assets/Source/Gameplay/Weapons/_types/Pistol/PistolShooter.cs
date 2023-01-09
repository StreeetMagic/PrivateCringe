using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class PistolShooter : Shooter
    {
        protected override IEnumerator Shooting()
        {
            IsShooting = true;
            
            print("Начали корутину стрельбы");

            for (int i = 0; i < Magazine.Bullets; i++)
            {
                var pause = new WaitForSeconds(MainFireCooldown);

                Magazine.LoseBullets(1);
                ShootSingleBullet();

                yield return pause;
            }
            
            IsShooting = false;
            
            if (Reloader.CanReload)
            {
                Reloader.Reload();
            }
        }
    }
}