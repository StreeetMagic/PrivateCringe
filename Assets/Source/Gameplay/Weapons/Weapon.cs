using Gameplay.Interfaces;
using Gameplay.Players.Scripts;
using Gameplay.Weapons.Magazines;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponReloader WeaponReloader { get; private set; }
        [field: SerializeField] public WeaponShooter WeaponShooter { get; private set; }
        [field: SerializeField] public WeaponBandolier WeaponBandolier { get; private set; }
        [field: SerializeField] public WeaponMagazine WeaponMagazine { get; private set; }
    }
}