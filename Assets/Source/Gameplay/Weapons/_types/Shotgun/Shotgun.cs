using Gameplay.Interfaces;

namespace Gameplay.Weapons.Shotgun
{
    public class Shotgun : Weapon
    {
        public override void Fire(ITargetable target)
        {
        
        }

        public override void Reload(ITargetable target)
        {
        
        }

        public override void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}
