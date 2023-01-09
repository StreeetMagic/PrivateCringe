using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons.Pistol
{
    public class PistolReloader : WeaponReloader
    {
        protected override IEnumerator Reloading()
        {
            IsReloading = true;
            yield return new WaitForSeconds(ReloadTime);
            WeaponMagazine.Fill(WeaponMagazine.MaxCapacity);
            WeaponBandolier.LoseBullets(WeaponMagazine.MaxCapacity);
            IsReloading = false;
        }
    }
}