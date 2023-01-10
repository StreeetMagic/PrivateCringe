using Gameplay.Players.Scripts;
using Gameplay.Weapons;
using UnityEngine;

namespace UI.HUD.ReloaderBars
{
    public class SwitchBar : ProgressBar
    {
        [SerializeField] private Weapon _weapon;
        
        private void OnEnable()
        {
            _weapon.SwitchingStarted += Draw;
        }

        private void OnDisable()
        {
            _weapon.SwitchingStarted -= Draw;
        }
    }
}