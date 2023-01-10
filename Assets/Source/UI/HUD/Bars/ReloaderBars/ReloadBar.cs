using Gameplay.Weapons;
using UnityEngine;

namespace UI.HUD.ReloaderBars
{
    public class ReloadBar : ProgressBar
    {
        [SerializeField] private Weapon Weapon;
        
        private void OnEnable()
        {
            Weapon.Reloader.ReloadStarted += Draw;
        }

        private void OnDisable()
        {
            Weapon.Reloader.ReloadStarted -= Draw;
        }
    }
}