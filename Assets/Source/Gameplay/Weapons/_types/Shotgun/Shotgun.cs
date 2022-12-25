using System.Collections;
using Gameplay.Interfaces;

namespace Gameplay.Weapons.Shotgun
{
    public class Shotgun : Weapon
    {
        public override bool TryFire()
        {
            return false;
        }

        protected override IEnumerator Firing()
        {
            throw new System.NotImplementedException();
        }

        protected override bool TryReload()
        {
            return false;
        }

        protected override IEnumerator Reloading()
        {
            throw new System.NotImplementedException();
        }
    }
}
