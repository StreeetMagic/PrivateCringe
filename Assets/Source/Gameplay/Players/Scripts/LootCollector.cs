using Gameplay.Loots;
using Gameplay.Loots.Ammos;
using UnityEngine;

namespace Gameplay.Players.Scripts
{
    public class LootCollector : MonoBehaviour
    {
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Loot loot))
            {
                if (loot.TryGetComponent(out Ammo ammo))
                {
                    ammo.PickUp();
                    Destroy(ammo.gameObject);
                }
            }
        }
    }
}