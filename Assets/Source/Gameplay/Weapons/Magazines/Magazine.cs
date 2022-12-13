using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Magazine : MonoBehaviour
    {
        [field: SerializeField] public int MaxCapacity { get; private set; }
        [field: SerializeField] public int Bullets { get; private set; }

        public bool IsEmpty => Bullets == 0;

        public int Fill(int count)
        {
            if (count > 0)
            {
                Bullets += count;
            }

            return Bullets;
        }

        public bool TryGetBullet()
        {
            if (Bullets >= 1)
            {
                Bullets--;

                return true;
            }

            return false;
        }
    }
}