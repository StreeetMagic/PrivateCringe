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

        public void Push(Vector3 direction)
        {
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