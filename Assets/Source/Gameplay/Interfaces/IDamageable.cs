using System;

namespace Gameplay.Interfaces
{
    public interface IDamageable
    {
        public int Health { get;}
        public int MaxHealth { get;}

        public void TakeDamage(int damage);

        public void Die();
        
        public event Action <int, int> HealthChanged;

    }
}