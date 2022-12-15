using AYellowpaper;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Humans.Players
{
    public class TargetFollower : MonoBehaviour
    {
        public InterfaceReference<ITargetable, MonoBehaviour> Enemy;

        private void Update()
        {
            Follow();
        }

        private void Follow()
        {
            transform.LookAt(Enemy.Value.Position);
        }
    }
}