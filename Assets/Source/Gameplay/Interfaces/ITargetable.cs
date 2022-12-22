using System;
using Gameplay.Humans.Players.TargetFinders;
using UnityEngine;

namespace Gameplay.Interfaces
{
    public interface ITargetable
    {
        public Vector3 Position { get; }
        public bool IsTargeted { get; set; }
        public void SetTargetedOn();
        public void SetTargetedOff();
        public event Action Missed;
    }
}