using System;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Bullet : MonoBehaviour

    {
        private ITargetable _target;
        
        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private float _power = 1000f;

        public void Push(ITargetable target)
        {
            print("выстрелил");
            _target = target;
            
            var direction = _target.Position - transform.position;
            _rigidbody.AddForce(direction * _power, ForceMode.Acceleration);
            
        }
    }
}