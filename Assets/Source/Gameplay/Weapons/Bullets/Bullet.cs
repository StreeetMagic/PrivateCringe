using System;
using Gameplay.Interfaces;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Weapons.Bullets
{
    public class Bullet : MonoBehaviour, IDealDamage

    {
        private ITargetable _target;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _power = 1000f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _maxOffset = 20;

        private  Random _random;

        public void Push(ITargetable target)
        {
            _target = target;
            
            
            _random = new Random();

            var offset1 = _random.Next(1, _maxOffset) / 10;
            var offset2 = _random.Next(1, _maxOffset) / 10;
            var offset3 = _random.Next(1, _maxOffset) / 10;

            var direction = (_target.Position - transform.position).normalized;

            
            
            
            _rigidbody.AddForce(direction * _power, ForceMode.Acceleration);
            
            
        }



        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                DealDamage(damageable);
                gameObject.SetActive(false);
            }
        }

        public int DealDamage(IDamageable damageable)
        {
            damageable.TakeDamage(_damage);

            return _damage;
        }
    }
}