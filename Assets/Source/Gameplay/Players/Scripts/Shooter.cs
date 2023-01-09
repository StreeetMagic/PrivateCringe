using System.Collections;
using UnityEngine;

namespace Gameplay.Players.Scripts
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private WeaponSwitcher _weaponSwitcher;

        private Coroutine _waitAndFire;

        private bool _waiting;

        public void TryFire()
        {
            if (_weaponSwitcher.CurrentWeapon.WeaponShooter.IsShooting == false)
            {
                _waitAndFire = StartCoroutine(WaitAndFire());
            }
        }

        private IEnumerator WaitAndFire()
        {
            _waiting = true;

            yield return new WaitForSeconds(.05f);
            _weaponSwitcher.CurrentWeapon.WeaponShooter.TryShoot();
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