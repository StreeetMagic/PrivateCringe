using System;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Crates
{
    public class Crate : MonoBehaviour, ITargetable, IDamageable
    {
        [SerializeField] private int _health = 1;
        [SerializeField] private int _maxHealth = 1;
        [SerializeField] private Outline _outline;

        public Vector3 Position =>
            transform.position;

        public bool IsTargeted { get; set; }

        public int Health =>
            _health;

        public int MaxHealth =>
            _maxHealth;

        public event Action<int, int> HealthChanged;

        public void SetTargetedOn()
        {
            IsTargeted = true;
            _outline.enabled = true;
        }

        public void SetTargetedOff()
        {
            IsTargeted = false;
            _outline.enabled = false;
        }

        private void Start()
        {
            HealthChanged?.Invoke(_health, _maxHealth);
        }

        public void TakeDamage(int damage)
        {
            if (damage < 1)
                return;

            _health -= damage;

            if (_health <= 0)
                _health = 0;

            HealthChanged?.Invoke(_health, _maxHealth);
            Die();
        }

        public void Die()
        {
            Debug.Log("Умер");
        }
    }
}