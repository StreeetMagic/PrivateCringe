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
    }
}
