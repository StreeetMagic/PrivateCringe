using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Players.Scripts
{
    public class Reloader : MonoBehaviour
    {
        [SerializeField] private Shooter Shooter;


        public bool TryReload(Weapon weapon)
        {
            if (weapon.WeaponReloader.CanReload)
            {
                Shooter.Stop();
                weapon.WeaponReloader.Reload();

                return true;
            }

            return false;
        }
    }
}