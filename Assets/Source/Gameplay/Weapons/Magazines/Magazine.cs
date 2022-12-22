﻿using System;
using UnityEngine;

namespace Gameplay.Weapons.Magazines
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

        private void Start()
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

        public bool TryFire()
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