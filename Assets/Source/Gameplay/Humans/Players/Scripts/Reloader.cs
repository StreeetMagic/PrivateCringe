using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Humans.Players
{
    public class Reloader : MonoBehaviour
    {
        [SerializeField] private Shooter Shooter;


        public bool TryReload(Weapon weapon)
        {
            if (weapon.CanReload)
            {
                print("Перезарядка началась");
                Shooter.Stop();
                weapon.Reload();

                return true;
            }

            return false;
        }

        public void OnReloaded()
        {
            Shooter.TryFire();
        }
    }
}