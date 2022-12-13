using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Gameplay.Humans.Players
{
    public class TargetFollower : MonoBehaviour
    {
        [SerializeField] private Enemy _target;

        private void Update()
        {
            var direction = _target.transform.position - transform.position;
            transform.forward = direction;
        }
    }
    
}
