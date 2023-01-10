using System;
using Gameplay.Interfaces;
using Gameplay.Players.Scripts;
using Gameplay.Weapons.Magazines;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField] public Reloader Reloader { get; private set; }
        [field: SerializeField] public Shooter Shooter { get; private set; }
        [field: SerializeField] public Bandolier Bandolier { get; private set; }
        [field: SerializeField] public Magazine Magazine { get; private set; }

        public Action <float> SwitchingStarted;
    }
}