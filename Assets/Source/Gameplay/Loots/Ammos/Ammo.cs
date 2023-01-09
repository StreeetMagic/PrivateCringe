using System;
using Gameplay.Weapons;
using UnityEngine;

namespace Gameplay.Loots.Ammos
{
    public class Ammo : Loot
    {
        [field:SerializeReference] public Weapon Weapon { get; private set; }

        public int Bullets { get; private set; }

        private void Start()
        {
            Bullets = 15;
        }

        public void PickUp()
        {
            Weapon.GainBullets(Bullets);
        }
    }
}