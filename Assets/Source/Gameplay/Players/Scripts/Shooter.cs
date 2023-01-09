using System.Collections;
using Gameplay.Interfaces;
using Gameplay.Players.TargetFinders;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Players.Scripts
{
    public class Shooter : MonoBehaviour
    {
        private const float SwitchRange = 156f;

        [SerializeField] private WeaponSwitcher WeaponSwitcher;
        [SerializeField] private TargetFinder _targetFinder;
        [SerializeField] private Reloader Reloader;

        private Coroutine _waitAndFire;

        private Weapon CurrentWeapon => WeaponSwitcher.CurrentWeapon;
        private Weapon Pistol => WeaponSwitcher.Pistol;
        private Weapon Famas => WeaponSwitcher.Famas;
        private Weapon Shotgun => WeaponSwitcher.Shotgun;
        
        private bool _waiting;

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

        private void OnTargetSet(ITargetable target)
        {
            if (WeaponSwitcher.CurrentWeapon.IsReloading || WeaponSwitcher.IsSwitching)
            {
                return;
            }

            var direction = target.Position - transform.position;
            var sqrDistance = Vector3.SqrMagnitude(direction);

            if (sqrDistance >= SwitchRange)
            {
                if (CurrentWeapon != Famas && Famas.CanFire)
                    WeaponSwitcher.SwitchTo(Famas);

                else if (CurrentWeapon == Famas && CurrentWeapon.CanFire)
                    TryFire();

                else if (CurrentWeapon != Pistol && CurrentWeapon.CanFire == false)
                    WeaponSwitcher.SwitchTo(WeaponSwitcher.Pistol);

                else if (CurrentWeapon == Pistol && CurrentWeapon.CanFire)
                    TryFire();
            }
            else
            {
                if (CurrentWeapon != Shotgun && Shotgun.CanFire)
                    WeaponSwitcher.SwitchTo(WeaponSwitcher.Shotgun);

                else if (CurrentWeapon == Shotgun && CurrentWeapon.CanFire)
                    TryFire();

                else if (CurrentWeapon != Pistol && CurrentWeapon.CanFire == false)
                    WeaponSwitcher.SwitchTo(Pistol);

                else if (CurrentWeapon == Pistol && CurrentWeapon.CanFire)
                    TryFire();
            }
        }

        public void TryFire()
        {
            if (CurrentWeapon.IsFiring == false)
            {
                _waitAndFire = StartCoroutine(WaitAndFire());
            }
        }

        private void OnTargetLost()
        {
            CurrentWeapon.Stop();

            Stop();
        }

        private IEnumerator WaitAndFire()
        {
            _waiting = true;

            yield return new WaitForSeconds(.05f);
            CurrentWeapon.TryFire();
            _waiting = false;
        }

        public void Stop()
        {
            if (_waitAndFire != null && _waiting)
            {
                StopCoroutine(_waitAndFire);
                _waiting = false;
                _waitAndFire = null;
            }
        }
    }
}