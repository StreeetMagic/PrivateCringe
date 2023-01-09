using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Famas
{
    public class FamasShooter : Shooter
    {
        [SerializeField] private int _fireCount = 3;
        [SerializeField] private float _tripleShotCooldown = 1.06f;

        protected override IEnumerator Shooting()
        {
            var pause = new WaitForSeconds(MainFireCooldown);
            var smallPause = new WaitForSeconds(_tripleShotCooldown);

            IsShooting = true;

            print("Начали корутину стрельбы");

            var remainder = Magazine.Bullets % _fireCount;
            print("Остаток от деления" + remainder);

            if (remainder > 0)
            {
                Magazine.LoseBullets(remainder);
                Bandolier.AddBullets(remainder);
            }

            var shoots = Magazine.Bullets / _fireCount;

            print("на начало стрельбы в магазине " + Magazine.Bullets);
            
            for (int i = 0; i < shoots; i++)
            {
                for (int j = 0; j < _fireCount; j++)
                {
                    Magazine.LoseBullets(1);
                    ShootSingleBullet();

                    yield return smallPause;
                }

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