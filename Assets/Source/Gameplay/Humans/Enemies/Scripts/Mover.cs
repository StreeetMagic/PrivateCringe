using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Humans.Enemies.Scripts
{
    public class Mover : MonoBehaviour, IChangePosition
    {
        public Vector3 Position => transform.position;
    }
}