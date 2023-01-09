using System;
using UnityEngine;

namespace Gameplay.Weapons.Magazines
{
    public class Magazine : MonoBehaviour
    {
        
        
        [field: SerializeField] public int MaxCapacity { get; private set; }
        [field: SerializeField] public int Bullets { get; private set; }

        public bool IsFull => Bullets >= MaxCapacity;

        public event Action <int> BulletsChanged;

        private void Start()
        {
            BulletsChanged?.Invoke(Bullets);
        }

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


        public void LoseBullets(int count)
        {
            Bullets -= count;

            if (Bullets < 0)
            {
                Bullets = 0;
            }
            
            BulletsChanged?.Invoke(Bullets);
        }

        public void Clear()
        {
            Bullets = 0;
            BulletsChanged?.Invoke(Bullets);
        }
    }
}