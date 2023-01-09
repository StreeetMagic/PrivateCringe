using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Famas
{
    public class FamasShooter : Shooter
    {

        [SerializeField] private float _tripleShotCooldown = 0.06f;

        public new bool CanShoot => Weapon.Magazine.Bullets >= 3;

        protected override IEnumerator Shooting()
        {
            var pause = new WaitForSeconds(MainFireCooldown);
            var smallPause = new WaitForSeconds(_tripleShotCooldown);

            IsShooting = true;


            int remainder = Weapon.Magazine.Bullets % _shootQueue;

            if (remainder > 0)
            {
                Weapon.Magazine.LoseBullets(remainder);
                Weapon.Bandolier.AddBullets(remainder);
            }

            var shoots = Weapon.Magazine.Bullets / _shootQueue;

            
            for (int i = 0; i < shoots; i++)
            {
                for (int j = 0; j < _shootQueue; j++)
                {
                    Weapon.Magazine.LoseBullets(1);
                    ShootSingleBullet();

                    yield return smallPause;
                }

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