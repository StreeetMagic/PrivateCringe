using System;
using System.Collections;
using Gameplay.Weapons.Magazines;
using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class WeaponReloader : MonoBehaviour
    {
        [SerializeField] protected float ReloadTime = 1;

        [field: SerializeField] public Weapon Weapon { get; private set; }

        protected WeaponBandolier WeaponBandolier => Weapon.WeaponBandolier;
        protected WeaponMagazine WeaponMagazine => Weapon.WeaponMagazine;
        private WeaponShooter WeaponShooter => Weapon.WeaponShooter;

        protected Coroutine _reloadingCoroutine;

        public bool IsReloading { get; protected set; }

        public event Action<float> ReloadStarted;

        public bool CanReload =>
            WeaponBandolier.Bullets > 0 &&
            WeaponMagazine.IsEmpty &&
            IsReloading == false;

        public void Reload()
        {
            WeaponShooter.Stop();
            ReloadStarted?.Invoke(ReloadTime);
            _reloadingCoroutine = StartCoroutine(Reloading());
        }

        protected abstract IEnumerator Reloading();
    }
}