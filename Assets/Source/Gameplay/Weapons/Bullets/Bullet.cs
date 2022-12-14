using System;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Bullet : MonoBehaviour

    {
        private Transform _target;

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            var direction = _target.transform.position - transform.position;
            transform.Translate(direction * 0.01f);
        }

        public void Init(Transform target)
        {
            _target = target;
        }
    }
}