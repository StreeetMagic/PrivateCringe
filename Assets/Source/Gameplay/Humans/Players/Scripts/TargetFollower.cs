using AYellowpaper;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Humans.Players
{
    public class TargetFollower : MonoBehaviour
    {
        public InterfaceReference<IChangePosition, MonoBehaviour> Enemy;

        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            var direction = Enemy.Value.Position - transform.position;
            transform.forward = direction;
        }
    }
    
}
