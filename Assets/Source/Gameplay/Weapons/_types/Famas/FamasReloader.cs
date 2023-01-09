﻿using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Famas
{
    public class FamasReloader : WeaponReloader
    {
        
        protected override IEnumerator Reloading()
        {
            IsReloading = true;

            yield return new WaitForSeconds(ReloadTime);

            if (WeaponBandolier.Bullets >= WeaponMagazine.MaxCapacity)
            {
                WeaponMagazine.Fill(WeaponMagazine.MaxCapacity);
                WeaponBandolier.LoseBullets(WeaponMagazine.MaxCapacity);
            }
            else
            {
                WeaponMagazine.Fill(WeaponBandolier.Bullets);
                WeaponBandolier.Clear();
            }

            IsReloading = false;
            StopCoroutine(_reloadingCoroutine);
        }
    }
}