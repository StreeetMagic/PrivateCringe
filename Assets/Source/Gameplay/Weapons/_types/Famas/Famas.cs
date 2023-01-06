using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons.Famas
{
    public class Famas : Weapon
    {
        [SerializeField] private int _fireCount = 3;
        [SerializeField] private float _tripleShotCooldown = 0.06f;

       

        protected override IEnumerator Firing()
        {
            IsFiring = true;

            while (CanFire)
            {
                for (int i = 0; i < _fireCount; i++)
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
                        yield return new WaitForSeconds(_tripleShotCooldown);
                    }
                }
                yield return new WaitForSeconds(MainFireCooldown);
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