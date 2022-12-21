using UnityEngine;

namespace Gameplay.Interfaces
{
    public interface ITargetable
    {
        public Vector3 Position { get; }
        public bool IsTargeted { get; set; }
        public void SetTargetedOn();
        public void SetTargetedOff();

    }
}