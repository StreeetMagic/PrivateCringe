using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Magazine : MonoBehaviour
    {
        [field: SerializeField] public int MaxCapacity { get; private set; }
        [field: SerializeField] public int Bullets { get; private set; }

        public bool IsEmpty => Bullets == 0;

        public event Action <int> BulletsChanged;

        private void OnEnable()
        {
            BulletsChanged?.Invoke(Bullets);
        }

        public int Fill(int count)
        {
            if (count > 0)
            {
                Bullets += count;
                BulletsChanged?.Invoke(Bullets);
            }

            return Bullets;
        }

        public bool TryGetBullet()
        {
            if (Bullets >= 1)
            {
                Bullets--;
                BulletsChanged?.Invoke(Bullets);
                return true;
            }

            return false;
        }
    }
}