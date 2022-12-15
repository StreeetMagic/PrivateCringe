using System;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Bullet : MonoBehaviour

    {
        
        
        private ITargetable _target;

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            var direction = _target.Position - transform.position;
            transform.Translate(direction * 0.01f);
        }

        public void Init(ITargetable target)
        {
            _target = target;
        }
    }
}