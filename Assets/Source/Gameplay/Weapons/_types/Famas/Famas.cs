using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class Famas : Weapon
    {
        private const int FireCount = 3;

        private float _smallCooldown = 0.06f;
        private float _largeCooldown = 0.5f;

        public override void Fire(ITargetable target)
        {
            if (Magazine.Bullets < FireCount)
                return;

            if (CanFire == false)
                return;

            CanFire = false;
            
            for (int i = 0; i < FireCount; i++)
            {
                FireSingleShot();
                StartCoroutine(Wait(_smallCooldown));
                FireSingleShot();
                StartCoroutine(Wait(_smallCooldown));
                FireSingleShot();
                StartCoroutine(Wait(_largeCooldown));
            }

            CanFire = true;
        }

        public override void Reload()
        {
        }

        public override void Stop()
        {
            throw new System.NotImplementedException();
        }

        private void FireSingleShot()
        {
            Debug.Log("Выстрелил");
        }

        private IEnumerator Wait(float pause)
        {
            yield return new WaitForSeconds(pause);
        } 
    }
}