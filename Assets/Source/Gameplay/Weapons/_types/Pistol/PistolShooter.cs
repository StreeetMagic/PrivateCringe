using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class PistolShooter : Shooter
    {
        protected override IEnumerator Shooting()
        {
            var pause = new WaitForSeconds(MainFireCooldown);
           
            IsShooting = true;
            
            var currentBullets = Weapon.Magazine.Bullets;

            for (int i = 0; i < currentBullets; i++)
            {
                Weapon.Magazine.LoseBullets(1);
                ShootSingleBullet();

                yield return pause;
            }
            
            IsShooting = false;
            
            if (Weapon.Reloader.CanReload)
            {
                
                Weapon.Reloader.Reload();
            }
        }
    }
}