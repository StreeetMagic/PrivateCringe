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

        [SerializeField] private TargetFinder _targetFinder;
        [SerializeField] private WeaponSwitcher _weaponSwitcher;

        private void OnEnable()
        {
            _targetFinder.TargetSet += OnTargetSet;
            _targetFinder.TargetLost += OnTargetLost;
        }

        private void OnDisable()
        {
            _targetFinder.TargetSet -= OnTargetSet;
            _targetFinder.TargetLost -= OnTargetLost;
        }

        private void Update()
        {
            bool isReloading = _weaponSwitcher.CurrentWeapon.Reloader.IsReloading;
            bool isSwitching = _weaponSwitcher.IsSwitching;

            if (isReloading || isSwitching)
            {
                return;
            }
            
            TryReload();

        }

        private void OnTargetSet(ITargetable target)
        {
            bool isReloading = _weaponSwitcher.CurrentWeapon.Reloader.IsReloading;
            bool isSwitching = _weaponSwitcher.IsSwitching;

            if (isReloading || isSwitching)
            {
                return;
            }

            var direction = target.Position - transform.position;
            var sqrDistance = Vector3.SqrMagnitude(direction);
            bool interacted;
            
            if (sqrDistance >= SwitchRange)
            {
                interacted = InteractWith(_weaponSwitcher.Famas);
            }
            else
            {
                interacted = InteractWith(_weaponSwitcher.Shotgun);
            }

            if (interacted == false)
            {
                InteractWith(_weaponSwitcher.Pistol);
            }
            
        }

        private bool InteractWith(Weapon weapon)
        {
            var currentWeapon = _weaponSwitcher.CurrentWeapon;

            if (currentWeapon != weapon && weapon.Shooter.CanShoot)
            {
                _weaponSwitcher.SwitchTo(weapon);
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
            _weaponSwitcher.CurrentWeapon.Shooter.StopShooting();
        }

        private bool TryReload()
        {
            Weapon[] weapons = new Weapon[3];
            weapons[0] = _weaponSwitcher.Pistol;
            weapons[1] = _weaponSwitcher.Famas;
            weapons[2] = _weaponSwitcher.Shotgun;

            foreach (var weapon in weapons)
            {
                if (weapon.Reloader.CanReload)
                {
                    if (_weaponSwitcher.CurrentWeapon != weapon)
                    {
                        _weaponSwitcher.SwitchTo(weapon);

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