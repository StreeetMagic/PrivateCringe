using System.Collections;
using Gameplay.Humans.Players.TargetFinders;
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

        private Coroutine _waitAndFire;

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
            _famas.gameObject.SetActive(true);
            _shotgun.gameObject.SetActive(false);
            _pistol.gameObject.SetActive(false);
            _currentWeapon = _famas;
        }

        private void OnTargetSet(ITargetable target)
        {
            if (_waitAndFire == null)
            {
                _waitAndFire = StartCoroutine(WaitAndFire(target));
            }
        }

        private void OnTargetLost()
        {
            _currentWeapon.Stop();

            Stop();
        }

        private IEnumerator WaitAndFire(ITargetable target)
        {
            yield return new WaitForSeconds(.05f);
            _currentWeapon.TryFire(target);
        }

        public void Stop()
        {
            if (_waitAndFire != null)
            {
                StopCoroutine(_waitAndFire);
                _waitAndFire = null;
            }
        }
    }
}