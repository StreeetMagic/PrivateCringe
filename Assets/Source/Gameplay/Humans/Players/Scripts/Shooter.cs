using System;
using System.Collections;
using AYellowpaper;
using Gameplay.Interfaces;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Humans.Players
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private Weapon _pistol;
        [SerializeField] private Weapon _famas;
        [SerializeField] private Weapon _shotgun;
        
        public InterfaceReference<ITargetable> TargetReference;
        public ITargetable Target => TargetReference.Value;

        public Action GotTarget;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _famas.gameObject.SetActive(false);
            _shotgun.gameObject.SetActive(false);
            _pistol.gameObject.SetActive(true);
            _currentWeapon = _pistol;
        }

        private void Shoot()
        {
            _currentWeapon.Fire(Target);
        }
    }
}
