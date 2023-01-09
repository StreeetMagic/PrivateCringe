using System;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Bandolier : MonoBehaviour
    {
        [field: SerializeField] public int Bullets { get; protected set; }

        public Action<int> BulletsChanged;

        private void Start()
        {
            BulletsChanged?.Invoke(Bullets);
        }

        public void AddBullets(int count)
        {
            if (count > 0)
            {
                Bullets += count;
                BulletsChanged?.Invoke(Bullets);
            }
        }

        public void LoseBullets(int count)
        {
            if (count > 0)
            {
                Bullets -= count;
                BulletsChanged?.Invoke(Bullets);
            }
        }

        public void Clear()
        {
            Bullets = 0;
            BulletsChanged?.Invoke(Bullets);
        }
    }
}