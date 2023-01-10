using System;
using System.Collections;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Players.Scripts
{
    public class WeaponSwitcher : MonoBehaviour
    {
        private const float SwitchDuration = .5f;
        
        [field: SerializeField] public Weapon Pistol { get; private set; }
        [field: SerializeField] public Weapon Famas { get; private set; }
        [field: SerializeField] public Weapon Shotgun { get; private set; }
        public Weapon CurrentWeapon { get; private set; }
        public bool IsSwitching { get; private set; }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            Pistol.gameObject.SetActive(true);
            Shotgun.gameObject.SetActive(true);
            Famas.gameObject.SetActive(true);

            Shotgun.gameObject.SetActive(false);
            Famas.gameObject.SetActive(false);

            CurrentWeapon = Pistol;
        }

        public void SwitchTo(Weapon weapon)
        {
            StartCoroutine(Switching(weapon));
        }

        private IEnumerator Switching(Weapon weapon)
        {
            IsSwitching = true;
            weapon.SwitchingStarted.Invoke(SwitchDuration);

            Famas.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(false);

            yield return new WaitForSeconds(SwitchDuration);

            weapon.gameObject.SetActive(true);
            CurrentWeapon = weapon;

            IsSwitching = false;
        }
    }
}