using Gameplay.Weapons;
using TMPro;
using UnityEngine;

namespace UI.HUD
{
    public class BulletDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private Weapon _weapon;

        [SerializeField] private int _magazineBullets;
        [SerializeField] private int _bullets;

        private void OnEnable()
        {
            _weapon.WeaponBandolier.BulletsChanged += SetBullets;
            _weapon.WeaponMagazine.BulletsChanged += SetMagazineBullets;
        }

        private void OnDisable()
        {
            _weapon.WeaponBandolier.BulletsChanged -= SetBullets;
            _weapon.WeaponMagazine.BulletsChanged -= SetMagazineBullets;
        }

        private void SetBullets(int count)
        {
            _bullets = count;
            ChangeText();
        }

        private void SetMagazineBullets(int count)
        {
            _magazineBullets = count;
            ChangeText();
        }

        private void ChangeText()
        {
            var text = _magazineBullets + " / " + _bullets;
            Text.text = text;
        }
    }

}