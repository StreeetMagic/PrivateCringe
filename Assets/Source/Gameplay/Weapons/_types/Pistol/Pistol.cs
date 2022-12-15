using Gameplay.Interfaces;
using Gameplay.Weapons;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Fire(ITargetable target)
    {
        if (Magazine.TryFire())
        {
            var bullet = Instantiate(
                BulletPrefab,
                ShootingPoint.transform.position,
                Quaternion.identity);

            bullet.Init(target);
        }
        else
        {
            Reload();
        }

    }

    public override void Reload()
    {
        if (Bullets >= Magazine.MaxCapacity)
        {
            Magazine.Fill(Magazine.MaxCapacity);
            Bullets -= Magazine.MaxCapacity;
            BulletsChanged?.Invoke(Bullets);
        }
        else if (Bullets == 0)
        {
            Debug.Log("Нет патронов");
        }
        else
        {
            var count = Bullets;
            Magazine.Fill(count);
            Bullets -= count;
            BulletsChanged?.Invoke(Bullets);
        }
    }
}