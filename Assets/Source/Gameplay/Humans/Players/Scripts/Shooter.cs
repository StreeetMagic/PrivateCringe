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
        
        public InterfaceReference<ITargetable> Target;
        
        private ITargetable _target => Target.Value;

        private void Start()
        {
            _famas.gameObject.SetActive(false);
            _shotgun.gameObject.SetActive(false);
            _currentWeapon = _pistol;
            _pistol.gameObject.SetActive(true);
            StartCoroutine(Pause());
        }

        private void Shoot()
        {
            _currentWeapon.Fire(_target);
        }

        private IEnumerator Pause()
        {

            while(true)
            {
                yield return new WaitForSeconds(.5f);
                Shoot();
            }
        }
    }

}
