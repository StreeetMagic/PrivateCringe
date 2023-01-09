using System.Collections;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Players.Scripts
{
    public class WeaponSwitcher : MonoBehaviour
    {
        public const float SwitchCooldown = .5f;
        
        [field:SerializeField] public Weapon Pistol { get; private set; }
        [field:SerializeField] public Weapon Famas{ get; private set; }
        [field:SerializeField] public Weapon Shotgun{ get; private set; }

        public Weapon CurrentWeapon { get; private set; }
        
        public bool IsSwitching { get; private set; }
        
        private Coroutine _switching;
        
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
            _switching = StartCoroutine(Switching(weapon));
        }

        private IEnumerator Switching(Weapon weapon)
        {
            IsSwitching = true;
            
            Famas.gameObject.SetActive(false);
            Pistol.gameObject.SetActive(false);
            Shotgun.gameObject.SetActive(false);


            yield return new WaitForSeconds(SwitchCooldown);

            weapon.gameObject.SetActive(true);
            CurrentWeapon = weapon;

            IsSwitching = false;
        }
    }
}
