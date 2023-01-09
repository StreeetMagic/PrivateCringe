using System;
using System.Collections;
using Gameplay.Weapons.Magazines;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Reloader : MonoBehaviour
    {
        [SerializeField] protected float ReloadTime = 1;

        [field: SerializeField] public Weapon Weapon { get; private set; }

        private Bandolier Bandolier => Weapon.Bandolier;
        private Magazine Magazine => Weapon.Magazine;
        private Shooter Shooter => Weapon.Shooter;

        private Coroutine _reloadingCoroutine;

        public bool IsReloading { get; protected set; }

        public event Action<float> ReloadStarted;

        public bool CanReload =>
            Bandolier.Bullets > 0 &&
            Magazine.IsFull == false &&
            IsReloading == false;

        public void Reload()
        {
            Shooter.StopShooting();
            ReloadStarted?.Invoke(ReloadTime);
            _reloadingCoroutine = StartCoroutine(Reloading());
        }

        private IEnumerator Reloading()
        {
            IsReloading = true;

            yield return new WaitForSeconds(ReloadTime);

            var remainingBullets = Magazine.MaxCapacity - Magazine.Bullets;
            
            
            if (Bandolier.Bullets >= remainingBullets)
            {
                Magazine.Fill(remainingBullets);
                Bandolier.LoseBullets(remainingBullets);
            }
            else
            {
                Magazine.Fill(Bandolier.Bullets);
                Bandolier.Clear();
            }

            IsReloading = false;
            StopCoroutine(_reloadingCoroutine);
        }
    }
}