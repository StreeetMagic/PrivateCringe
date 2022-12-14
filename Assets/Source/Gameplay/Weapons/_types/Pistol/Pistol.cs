using Gameplay.Weapons;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Fire(Transform target)
    {
        var bullet = Instantiate(
            BulletPrefab, 
            ShootingPoint.transform.position, 
            Quaternion.identity);
        bullet.Init(target);
    }

    public override void Reload()
    {
        if (Bullets >= Magazine.MaxCapacity)
        {
            Magazine.Fill(Magazine.MaxCapacity);
            BulletsChanged?.Invoke(Bullets);
        }
        else if (Bullets == 0)
        {
            Debug.Log("Нет патронов");
        }
        else
        {
            Magazine.Fill(Bullets);
            BulletsChanged?.Invoke(Bullets);
        }
    }

}