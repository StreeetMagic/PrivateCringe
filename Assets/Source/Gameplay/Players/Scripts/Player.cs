using System;
using Gameplay.Interfaces;
using Gameplay.Players.TargetFinders;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Players.Scripts
{
    public class Player : MonoBehaviour
    {
        private const float SwitchRange = 156f;

        [field: SerializeField] public TargetFinder TargetFinder { get; private set; }
        [field: SerializeField] public WeaponSwitcher WeaponSwitcher { get; private set; }

        private void OnEnable()
        {
            TargetFinder.TargetSet += OnTargetSet;
            TargetFinder.TargetLost += OnTargetLost;
        }

        private void OnDisable()
        {
            TargetFinder.TargetSet -= OnTargetSet;
            TargetFinder.TargetLost -= OnTargetLost;
        }

        private void Update()
        {
            bool isReloading = WeaponSwitcher.CurrentWeapon.Reloader.IsReloading;
            bool isSwitching = WeaponSwitcher.IsSwitching;
            bool isShooting = WeaponSwitcher.CurrentWeapon.Shooter.IsShooting;

            if (isReloading | isSwitching | isShooting)
            {
                return;
            }

            TryReload();
        }

        private void OnTargetSet(ITargetable target)
        {
            bool isReloading = WeaponSwitcher.CurrentWeapon.Reloader.IsReloading;
            bool isSwitching = WeaponSwitcher.IsSwitching;

            if (isReloading || isSwitching)
            {
                return;
            }

            var direction = target.Position - transform.position;
            var sqrDistance = Vector3.SqrMagnitude(direction);
            bool interacted;

            if (sqrDistance >= SwitchRange)
            {
                interacted = InteractWith(WeaponSwitcher.Famas);
            }
            else
            {
                interacted = InteractWith(WeaponSwitcher.Shotgun);
            }

            if (interacted == false)
            {
                InteractWith(WeaponSwitcher.Pistol);
            }
        }

        private bool InteractWith(Weapon weapon)
        {
            var currentWeapon = WeaponSwitcher.CurrentWeapon;

            if (currentWeapon != weapon && weapon.Shooter.CanShoot)
            {
                WeaponSwitcher.SwitchTo(weapon);

                return true;
            }

            if (currentWeapon == weapon && currentWeapon.Shooter.CanShoot)
            {
                currentWeapon.Shooter.TryShoot();

                return true;
            }

            return false;
        }

        private void OnTargetLost()
        {
            WeaponSwitcher.CurrentWeapon.Shooter.StopShooting();
        }

        private bool TryReload()
        {
            Weapon[] weapons = new Weapon[3];
            weapons[0] = WeaponSwitcher.Pistol;
            weapons[1] = WeaponSwitcher.Famas;
            weapons[2] = WeaponSwitcher.Shotgun;

            foreach (var weapon in weapons)
            {
                if (weapon.Reloader.CanReload)
                {
                    if (WeaponSwitcher.CurrentWeapon != weapon)
                    {
                        WeaponSwitcher.SwitchTo(weapon);

                        return false;
                    }

                    weapon.Reloader.Reload();

                    return true;
                }
            }

            return false;
        }
    }
}