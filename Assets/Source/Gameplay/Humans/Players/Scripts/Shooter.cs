using System.Collections;
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
        [SerializeField] private Transform _target;

        private void Start()
        {
            _famas.gameObject.SetActive(false);
            _shotgun.gameObject.SetActive(false);
            _currentWeapon = _pistol;
            _pistol.gameObject.SetActive(true);
            StartCoroutine(Pause());
        }

        public void Shoot()
        {
            _currentWeapon.Fire(_target);
        }

        private IEnumerator Pause()
        {

            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(1);
                Shoot();
            }
        }
    }

}
