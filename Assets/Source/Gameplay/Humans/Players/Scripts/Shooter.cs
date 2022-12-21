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
        [SerializeField] private TargetFinder _targetFinder;

        private void OnEnable()
        {
            _targetFinder.TargetSet += OnTargetSet;
            _targetFinder.TargetLost += OnTargetLost;
        }

        private void OnDisable()
        {
            _targetFinder.TargetSet -= OnTargetSet;
            _targetFinder.TargetLost -= OnTargetLost;;
        }

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

        private void OnTargetSet(ITargetable target)
        {
            _currentWeapon.Fire(target);
        }

        private void OnTargetLost()
        {
            _currentWeapon.Stop();
        }
        
    }
}