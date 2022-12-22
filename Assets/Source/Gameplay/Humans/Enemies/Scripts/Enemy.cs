using System;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Humans.Enemies.Scripts
{
    public class Enemy : MonoBehaviour, ITargetable, IDamageable
    {
        [SerializeField] private Outline _outline;
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;

        public event Action<int, int> HealthChanged;
        public event Action Died;
        public event Action Missed;

        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public bool IsTargeted { get; set; }
        public Vector3 Position => transform.position;

        private void Start()
        {
            HealthChanged?.Invoke(_health, _maxHealth);
        }

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

        public void TakeDamage(int damage)
        {
            if (damage < 1)
            {
                return;
            }

            _health -= damage;
            HealthChanged?.Invoke(_health, _maxHealth);

            if (_health <= 0)
            {
                _health = 0;
                Die();
            }
        }

        public void Die()
        {
            Missed?.Invoke();
            Died?.Invoke();

            gameObject.SetActive(false);
        }
    }
}