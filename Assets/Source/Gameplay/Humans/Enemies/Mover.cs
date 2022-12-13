using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Humans.Enemies
{
    public class Mover : MonoBehaviour, IChangePosition
    {
        public Vector3 Position 
            => transform.position;
    }
}